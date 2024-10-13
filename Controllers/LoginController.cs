using Microsoft.AspNetCore.Mvc;

using SistemaRedeWork.Models; // Certifique-se de incluir o namespace do seu modelo

using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;

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
            var login = await _context.LoginEstudantes
                .FirstOrDefaultAsync(l => l.Email == loginEstudante.Email && l.Senha == loginEstudante.Senha);

            if (login != null) {
                TempData["MensagemSucesso"] = $"Login realizado com sucesso! Bem-vindo!";
                return RedirectToAction("Index", "Home");
            }

            TempData["MensagemErro"] = $"E-mail ou senha inválidos! Tente novamente.";
        }

        return View(loginEstudante);
    }


}
