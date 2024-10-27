using Microsoft.AspNetCore.Mvc;

using SistemaRedeWork.Models; // Certifique-se de incluir o namespace do seu modelo
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text;

public class LoginController : Controller {
    private readonly BancoContext _context;


    // Construtor único para injeção de dependências
    public LoginController(BancoContext context) {
        _context = context;

    }

    [HttpGet]
    public IActionResult LoginEmpresa() {

        return View();
    }

    //[Authorize]
    //public IActionResult EmpresaLogado() {

    //    return View();
    //}

    [HttpPost]
    public async Task<IActionResult> LoginEmpresa(LoginEmpresaModel loginEmpresa) {
        // Verifica se o ModelState é inválido e exibe os erros no console
        if (!ModelState.IsValid) {
            foreach (var entry in ModelState) {
                foreach (var error in entry.Value.Errors) {
                    Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                }
            }
            return View(loginEmpresa); // Retorna a view com os erros
        }

        // Busca o usuário no banco de dados
        var login = await _context.LoginEmpresas
            .FirstOrDefaultAsync(l => l.Email == loginEmpresa.Email);

        if (login == null) {
            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
            return View(loginEmpresa); // Retorna a view caso o login falhe
        }

        // Verifique se a senha está correta (essa parte deve ser implementada)
        // Exemplo: if (!VerifyPassword(login, loginEmpresa.Senha)) { ... }

        // Adicione o ID da empresa nas claims
        var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, login.Email),
        new Claim("FullName", login.Email), // Pode ser outro dado relevante
        new Claim(ClaimTypes.Role, "User"),  // Definir o papel do usuário, se necessário
        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()), // ID do usuário
        new Claim("EmpresaId", login.EmpresaId.ToString()) // Aqui você deve ter a propriedade EmpresaId
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties {
            IsPersistent = loginEmpresa.RememberMe // Persistência do cookie se o usuário selecionar "lembrar-me"
        };

        // Configurar a autenticação do usuário
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";

        // Redireciona para a página de empresa logada após o login
        return RedirectToAction("EmpresaLogado", "Cadastro");
    }




    public IActionResult EmpresaLogado(int id) {
        var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
        if (empresa == null) {
            return NotFound(); // Retorna 404 se o estudante não for encontrado
        }

        // Preencher o modelo com os dados do estudante
        var model = new EmpresaModel {
            Id = empresa.Id,
            Usuario = empresa.Usuario

            // Adicione outros campos necessários
        };

        return View(model); // Certifique-se de retornar a view com o modelo preenchido
    }



    [HttpGet]
    public IActionResult LoginEstudante() {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> LoginEstudante(LoginEstudanteModel loginEstudante) {
        // Verifica se o modelo está válido
        if (!ModelState.IsValid) {
            foreach (var entry in ModelState) {
                foreach (var error in entry.Value.Errors) {
                    Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                }
            }
            // Retorna a view com os erros de validação
            return View(loginEstudante);
        }

        // Busca o usuário no banco de dados
        var login = await _context.LoginEstudantes
            .FirstOrDefaultAsync(l => l.Email == loginEstudante.Email && l.Senha == loginEstudante.Senha);

        // Verifica se o login foi encontrado
        if (login != null) {
            // Salva o nome do usuário na sessão
            HttpContext.Session.SetString("NomeUsuario", $"{login.Nome}");

            // Criar as claims do usuário
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, login.Email),
            new Claim("FullName", $"{login.Nome} {login.Sobrenome}"),
            new Claim(ClaimTypes.Role, "User"), // Pode definir o papel do usuário
            new Claim("EstudanteId", login.EstudanteId.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties {
                IsPersistent = loginEstudante.RememberMe // Persistência do cookie se o usuário selecionar "lembrar-me"
            };

            // Configurar a autenticação do usuário
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["MensagemSucesso"] = "Login realizado com sucesso! Bem-vindo!";
            return RedirectToAction("EstudanteLogado", new { id = login.Id }); // Redirecionar para EstudanteLogado com o ID
        }

        TempData["MensagemErro"] = "E-mail ou senha inválidos! Tente novamente.";
        return View(loginEstudante);
    }


    [HttpGet("estudante/logado")]
    public IActionResult EstudanteLogado() {
        return View();
    }

    [HttpGet("estudante/logado/{id}")]
    public IActionResult EstudanteLogado(int id) {
        var estudante = _context.Estudantes.FirstOrDefault(e => e.Id == id);
        if (estudante == null) {
            return NotFound(); // Retorna 404 se o estudante não for encontrado
        }

        // Buscar todas as vagas e suas respectivas empresas
        var vagas = _context.Vagas
            .Include(v => v.Empresa)  // Inclui os dados da empresa (se necessário)
            .Where(v => v.EmpresaId != null && v.Ativa)
            .ToList();

        // Criar uma lista de EmpresaEstudanteViewModel
        var model = new List<EmpresaEstudanteViewModel>();

        // Agrupar as vagas por empresa
        var vagasAgrupadas = vagas.GroupBy(v => v.EmpresaId);

        foreach (var grupo in vagasAgrupadas) {
            // Criar um modelo para cada grupo de vagas
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == grupo.Key);

            // Buscar o currículo do estudante (se existir)
            //var curriculo = _context.Curriculos.FirstOrDefault(c => c.EstudanteId == id);

            var viewModel = new EmpresaEstudanteViewModel {
                Empresa = empresa,
                Estudante = estudante,
                //Curriculo = curriculo,
                Vagas = grupo.ToList()
            };

            model.Add(viewModel);
        }

        return View(model);
    }





    private bool VerifyPassword(string inputPassword, string storedHashedPassword) {
        // Hasheia a senha de entrada e compara com a senha armazenada
        var hashedInputPassword = HashPassword(inputPassword);
        return hashedInputPassword == storedHashedPassword;
    }

    public string HashPassword(string password) {
        using (var sha256 = SHA256.Create()) {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(); // Faz o logout do usuário
        TempData["MensagemSucesso"] = "Você saiu com sucesso!";
        return RedirectToAction("LoginEstudante", "Login"); // Redireciona para a página de login ou outra página desejada
    }
}


