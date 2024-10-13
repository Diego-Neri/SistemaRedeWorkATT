using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;

public class CadastroController : Controller {

    private readonly BancoContext _context;

    public CadastroController(BancoContext context) {
        _context = context;
    }

    // Exibe o formulário de cadastro de empresa
    public IActionResult CadastroEmpresa() {
        return View();
    }

    // Processa o cadastro de empresa
    [HttpPost]
    public IActionResult CadastroEmpresa(EmpresaModel empresa) {
        if (ModelState.IsValid) {
            // Verifica se a senha e a confirmação de senha correspondem
            //if (empresa.Senha != empresa.ConfirmaSenha) {
            //    ModelState.AddModelError("ConfirmaSenha", "A senha e a confirmação de senha não correspondem.");
            //    return View(empresa);
            //}

            try {
                // Adiciona a empresa ao banco de dados
                _context.Empresas.Add(empresa);
                _context.SaveChanges();

                return RedirectToAction("Sucesso");  // Redireciona para uma página de sucesso
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                // Registra o erro e exibe uma mensagem apropriada
                ModelState.AddModelError("", "Ocorreu um erro ao cadastrar a empresa. Tente novamente.");
                return View(empresa);
            }
        }
        return View(empresa);   // Retorna a view com os dados preenchidos caso haja erro de validação
    }

    // Action para a página de sucesso
    public IActionResult Sucesso() {
        return View();
    }

    public IActionResult CadastroEstudante() {
        return View();
    }

    [HttpPost]

    public IActionResult CadastroEstudante(EstudanteModel estudante) {
        if (ModelState.IsValid) {
            //if(estudante.Senha != estudante.ConfirmarSenha) {
            //    TempData["MensagemErro"] = $"As senhas não conferem! Verifique e tente novamente.";
            //    return View(estudante);

            //}

            try {
                _context.Estudantes.Add(estudante);
                _context.SaveChanges();

                return RedirectToAction("Sucesso");
            }catch (Exception ex) {
                Console.WriteLine(ex.Message);

                TempData["MensagemErro"] = $"Ocorreu um erro, tente novamente!";
                return View(estudante);
            }
        }
        return View(estudante);
    }
}
