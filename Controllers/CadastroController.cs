using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using SistemaRedeWork.Controllers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
            if (empresa.Senha != empresa.ConfirmarSenha) {
                ModelState.AddModelError("ConfirmarSenha", "A senha e a confirmação de senha não correspondem.");
                return View(empresa);
            }

            // Verifica se o email já está cadastrado
            var emailExistente = _context.LoginEmpresas.Any(le => le.Email == empresa.Email);
            if (emailExistente) {
                ModelState.AddModelError("Email", "O e-mail já está em uso. Por favor, use outro e-mail.");
                return View(empresa);
            }

            try {
                // Adiciona a empresa ao banco de dados
                _context.Empresas.Add(empresa);
                _context.SaveChanges();

                // Cria o registro correspondente na tabela LoginEmpresa
                var loginEmpresa = new LoginEmpresaModel {
                    Email = empresa.Email,
                    Password = empresa.Senha,
                    EmpresaId = empresa.Id,
                    CNPJ = empresa.CNPJ,
                    RazaoSocial = empresa.RazaoSocial,
                    ResetCode = "" // Valor inicial para evitar erro de nulidade
                };

                _context.LoginEmpresas.Add(loginEmpresa);
                _context.SaveChanges();

                return RedirectToAction("EmpresaLogado");  // Redireciona para uma página de sucesso
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                // Registra o erro e exibe uma mensagem apropriada
                TempData["MensagemErro"] = $"Ocorreu um erro, tente novamente!";
                return View(empresa);
            }
        }
        return View(empresa);   // Retorna a view com os dados preenchidos caso haja erro de validação
    }


    public IActionResult EmpresaLogado() {
        var estudantes = _context.Estudantes
            .Include(e => e.Curriculo) // Inclui o Currículo
            .ToList();

        var model = estudantes.Select(e => new EmpresaEstudanteViewModel {
            Estudante = e,
            Curriculo = e.Curriculo // Associa o currículo ao view model
        }).ToList();

        return View(model);
    }




    public IActionResult ListarEstudantes() {
        // Buscando todos os estudantes do banco de dados
        var estudantes = _context.Estudantes.ToList();

        // Passando os estudantes para a View
        return View(estudantes);
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
            if (estudante.Senha != estudante.ConfirmarSenha) {
                ModelState.AddModelError("ConfirmarSenha", "A senha e a confirmação de senha não correspondem.");
                return View(estudante);
            }

            // Verifica se o email já está cadastrado
            var emailExistente = _context.LoginEstudantes.Any(le => le.Email == estudante.Email);
            if (emailExistente) {
                ModelState.AddModelError("Email", "O e-mail já está em uso. Por favor, use outro e-mail.");
                return View(estudante);
            }

            try {
                _context.Estudantes.Add(estudante);
                _context.SaveChanges();

                var loginEstudante = new LoginEstudanteModel {
                    Email = estudante.Email,
                    Senha = estudante.Senha,
                    EstudanteId = estudante.Id,
                    Nome = estudante.Nome,
                    Sobrenome = estudante.Sobrenome,
                    ResetCode = "" // Valor inicial para evitar erro de nulidade
                };
                _context.LoginEstudantes.Add(loginEstudante);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = $"Cadastro criado com sucesso. Seja Bem-Vindo!!";
                return RedirectToAction("EstudanteLogado", "Login");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);

                TempData["MensagemErro"] = $"Ocorreu um erro, tente novamente!";
                return View(estudante);
            }
        }
        return View(estudante);
    }

}
