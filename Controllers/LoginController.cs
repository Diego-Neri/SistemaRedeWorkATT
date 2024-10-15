using Microsoft.AspNetCore.Mvc;

using SistemaRedeWork.Models; // Certifique-se de incluir o namespace do seu modelo

using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

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
            var login = await _context.LoginEmpresas
                .FirstOrDefaultAsync(l => l.Email == loginEmpresa.Email && l.Password == loginEmpresa.Password);

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

    [Authorize]
    public IActionResult EmpresaLogado() {

        return View();
    }


    [HttpGet]
    public IActionResult LoginEstudante() {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> LoginEstudante(LoginEstudanteModel loginEstudante) {
        if (!ModelState.IsValid) {
            foreach (var entry in ModelState) {
                foreach (var error in entry.Value.Errors) {
                    Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                }
            }
        }

        if (ModelState.IsValid) {
            var login = await _context.LoginEstudantes
                .FirstOrDefaultAsync(l => l.Email == loginEstudante.Email && l.Senha == loginEstudante.Senha);

            if (login != null) {
                // Criar as claims do usuário
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, login.Email),
                new Claim("FullName", login.Email),
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

                TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";
                return RedirectToAction("EstudanteLogado", "Login");
            }

            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
        }

        return View(loginEstudante);
    }

    [Authorize]
    public IActionResult EstudanteLogado() {

        return View();
    }

}
