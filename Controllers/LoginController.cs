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

    [Authorize]
    public IActionResult EmpresaLogado() {

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
        }

        if (ModelState.IsValid) {
            // Busca o usuário no banco de dados
            var login = await _context.LoginEmpresas
                .FirstOrDefaultAsync(l => l.Email == loginEmpresa.Email);

            if (login != null) {
                // Criar as claims do usuário
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim("FullName", login.Email),
                new Claim(ClaimTypes.Role, "User") // Pode definir o papel do usuário
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
                return RedirectToAction("EmpresaLogado", "Login");
            }

            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
        }

        return View(loginEmpresa);
    }

    //private bool VerificaSenha(string inputPassword, string storedHashedPassword) {
    //    // Hasheia a senha de entrada e compara com a senha armazenada
    //    var hashedInputPassword = HashPasswordEmpresa(inputPassword);
    //    return hashedInputPassword == storedHashedPassword;
    //}

    //public string HashPasswordEmpresa(string password) {
    //    using (var sha256 = SHA256.Create()) {
    //        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    //        return Convert.ToBase64String(bytes);
    //    }
    //}








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
            new Claim(ClaimTypes.Role, "User") // Pode definir o papel do usuário
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


    public IActionResult EstudanteLogado(int id) {
        var estudante = _context.Estudantes.FirstOrDefault(e => e.Id == id);
        if (estudante == null) {
            return NotFound(); // Retorna 404 se o estudante não for encontrado
        }

        // Preencher o modelo com os dados do estudante
        var model = new EstudanteModel {
            Id = estudante.Id,
            Nome = estudante.Nome

            // Adicione outros campos necessários
        };

        return View(model); // Certifique-se de retornar a view com o modelo preenchido
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

}


