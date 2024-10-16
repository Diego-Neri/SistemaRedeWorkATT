using Microsoft.AspNetCore.Mvc;

using SistemaRedeWork.Models; // Certifique-se de incluir o namespace do seu modelo
using System.Security.Cryptography;

using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
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

            // Verifica se o usuário existe e compara a senha
            if (login != null && VerifyPassword(loginEmpresa.Password, login.Password)) {
                TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";
                return RedirectToAction("Index", "Home");
            }

            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
        }

        return View(loginEmpresa);
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
            // Busca o usuário no banco de dados
            var login = await _context.LoginEstudantes
                .FirstOrDefaultAsync(l => l.Email == loginEstudante.Email);

            // Verifica se o usuário existe e compara a senha
            if (login != null && VerifyPassword(loginEstudante.Senha, login.Senha)) {
                TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";
                return RedirectToAction("Index", "Home");
            }

            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
        }

        return View(loginEstudante);
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


