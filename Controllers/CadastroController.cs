using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using SistemaRedeWork.Controllers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;

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

    public IActionResult CadastrarVagas(int id) {
        var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
        if (empresa == null) {
            return NotFound();
        }

        var model = new CadastrarVagasViewModel {
            CadastrarVagas = new CadastrarVagasModel { EmpresaId = empresa.Id }, // Define o EmpresaId
            Empresas = _context.Empresas.ToList(), // Preencher a lista de empresas, se necessário
            RazaoSocial = empresa.RazaoSocial // Obtém a razão social da empresa

        };

        return View(model);
    }






    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CadastrarVagas(CadastrarVagasViewModel model) {
        var vaga = model.CadastrarVagas; // Acesse o modelo CadastrarVagas a partir do view model

        // Verifica se a empresa existe
        var empresaExists = await _context.Empresas.AnyAsync(e => e.Id == vaga.EmpresaId);
        if (!empresaExists) {
            ModelState.AddModelError("EmpresaId", "A empresa associada à vaga não existe.");

            // Preenche a lista de empresas novamente para mostrar na view
            model.Empresas = await _context.Empresas.ToListAsync();
            return View(model); // Retorna o ViewModel para a view
        }

        // Adiciona a nova vaga
        _context.Vagas.Add(vaga);
        await _context.SaveChangesAsync();
        TempData["MensagemSucesso"] = $"Vaga cadastrada com sucesso!";
        return RedirectToAction("MinhasVagasView");
    }

    public IActionResult MinhasVagasView() {
        // Obtém o ID da empresa a partir das claims do usuário
        var empresaIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        // Verifica se a claim "EmpresaId" foi encontrada
        if (empresaIdClaim == null) {
            // Retorna uma mensagem de erro e redireciona para a tela EmpresaLogado
            TempData["MensagemErro"] = "ID da empresa não encontrado.";
            return RedirectToAction("EmpresaLogado", "Cadastro");
        }

        // Tenta converter a claim para um int
        if (!int.TryParse(empresaIdClaim.Value, out int empresaId)) {
            // Retorna uma mensagem de erro e redireciona para a tela EmpresaLogado
            TempData["MensagemErro"] = "ID da empresa inválido.";
            return RedirectToAction("EmpresaLogado", "Cadastro");
        }

        // Filtra as vagas pela empresa
        var vagas = _context.Vagas.Where(v => v.EmpresaId == empresaId).ToList();

        // Se não houver vagas, adiciona uma mensagem informativa
        if (!vagas.Any()) {
            ViewBag.Mensagem = "Você ainda não cadastrou nenhuma vaga.";
        }

        return View(vagas); // Retorna a lista de vagas para a view
    }





    [HttpPost]
    public async Task<IActionResult> AtivarDesativar(int id) {
        // Busca a vaga pelo ID
        var vaga = await _context.Vagas.FindAsync(id);

        if (vaga == null) {
            TempData["MensagemErro"] = "Vaga não encontrada.";
            return RedirectToAction("MinhasVagasView");
        }

        // Alterna o status da vaga
        vaga.Ativa = !vaga.Ativa;

        // Atualiza a vaga no banco de dados
        _context.Vagas.Update(vaga);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = $"Vaga {(vaga.Ativa ? "ativada" : "desativada")} com sucesso!";
        return RedirectToAction("MinhasVagasView");
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id) {
        // Encontra a vaga pelo ID
        var vaga = await _context.Vagas.FindAsync(id);
        if (vaga == null) {
            TempData["MensagemErro"] = "Vaga não encontrada.";
            return RedirectToAction("MinhasVagasView");
        }

        // Remove a vaga
        _context.Vagas.Remove(vaga);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Vaga excluída com sucesso.";
        return RedirectToAction("MinhasVagasView");
    }

    [HttpGet]
    public IActionResult DownloadCurriculo(int id) {
        var arquivo = _context.Arquivos.FirstOrDefault(a => a.Id == id);
        if (arquivo == null) {
            TempData["MensagemErro"] = "Currículo não encontrado.";
            return RedirectToAction("MinhasVagasView");
        }

        return File(arquivo.Dados, arquivo.ContentType, arquivo.Descricao);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(); // Faz o logout do usuário
        TempData["MensagemSucesso"] = "Você saiu com sucesso!";
        return RedirectToAction("LoginEmpresa", "Login"); // Redireciona para a página de login ou outra página desejada
    }


}
