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

    [HttpGet]
    public IActionResult LoginEstudante() {
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> LoginEmpresa(LoginEmpresaModel loginEmpresa) {
        if (!ModelState.IsValid) {
            foreach (var entry in ModelState) {
                foreach (var error in entry.Value.Errors) {
                    Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                }
            }
            return View(loginEmpresa); 
        }
        // Hash da senha de entrada
        string senhaHash = HashPassword(loginEmpresa.Password);

        
        var hashArmazenado = await _context.LoginEstudantes
            .Where(l => l.Email == loginEmpresa.Email)
            .Select(l => l.Senha)
            .FirstOrDefaultAsync();
        Console.WriteLine($"Hash calculado: {senhaHash}");
        Console.WriteLine($"Hash armazenado: {hashArmazenado}");

        // Busca o usuário no banco de dados
        var login = await _context.LoginEmpresas
            .FirstOrDefaultAsync(l => l.Email == loginEmpresa.Email);

        if (login == null) {
            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
            return View(loginEmpresa); 
        }

        // Adicione o ID da empresa nas claims
        var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, login.Email),
        new Claim("FullName", login.Email), 
        new Claim(ClaimTypes.Role, "User"), 
        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()), 
        new Claim("Empresa", login.ID_EMPRESA.ToString()) 
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties {
            IsPersistent = loginEmpresa.RememberMe 
        };

        // Configurar a autenticação do usuário
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";
      
        return RedirectToAction("EmpresaLogado", "Cadastro");
    }


    public IActionResult EmpresaLogado(int id) {
        var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
        if (empresa == null) {
            return NotFound(); 
        }

        // Preencher o modelo com os dados do estudante
        var model = new EmpresaModel {
            Id = empresa.Id,
            Usuario = empresa.Usuario

        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LoginEstudante(LoginEstudanteModel loginEstudante) {
        if (!ModelState.IsValid) {
            foreach (var entry in ModelState) {
                foreach (var error in entry.Value.Errors) {
                    Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                }
            }
            return View(loginEstudante);
        }

        // Hash da senha de entrada
        string senhaHash = HashPassword(loginEstudante.Senha);

        // Busca o usuário no banco de dados com a senha hashada e incluindo a propriedade `Estudante`
        var login = await _context.LoginEstudantes
            .Include(le => le.Estudante)  // le representa cada `LoginEstudante`
            .FirstOrDefaultAsync(le => le.Email == loginEstudante.Email && le.Senha == senhaHash);

        if (login != null && login.Estudante != null) {
            HttpContext.Session.SetString("NomeUsuario", login.Nome);

            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, login.Email),
            new Claim("FullName", login.Nome),
            new Claim(ClaimTypes.Role, "User"),
            new Claim("Estudante", login.ID_ESTUDANTE.ToString())
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties {
                IsPersistent = loginEstudante.RememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["MensagemSucesso"] = "Login realizado com sucesso! Bem-vindo!";
            return RedirectToAction("EstudanteLogado", new { id = login.ID_ESTUDANTE });
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
        var loginEstudantes = _context.LoginEstudantes.FirstOrDefault(e => e.ID_ESTUDANTE == id);
        if (estudante == null) {
            return NotFound(); 
        }
        var curriculo = _context.Curriculo.FirstOrDefault(c => c.ID_ESTUDANTE == id);

        // Buscar todas as vagas e suas respectivas empresas
        var vagas = _context.Vagas
            .Include(v => v.Empresa)  // Inclui os dados da empresa (se necessário)
            .Where(v => v.ID_EMPRESA != null && v.Status)
            .ToList();

        // Criar uma lista de EmpresaEstudanteViewModel
        var model = new List<EmpresaEstudanteViewModel>();

        // Agrupar as vagas por empresa
        var vagasAgrupadas = vagas.GroupBy(v => v.ID_EMPRESA);

        foreach (var grupo in vagasAgrupadas) {
            // Criar um modelo para cada grupo de vagas
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == grupo.Key);


            var viewModel = new EmpresaEstudanteViewModel {
                Empresa = empresa,
                Estudante = estudante,
                Curriculo = curriculo,
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
        return RedirectToAction("LoginEstudante", "Login"); 
    }
}


