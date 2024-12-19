using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using SistemaRedeWork.Controllers;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;

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
                TempData["MensagemErro"] = $"ConfirmarSenha A senha e a confirmação de senha não correspondem.";
                return View(empresa);
            }

            // Verifica se o email já está cadastrado
            var emailExistente = _context.LoginEmpresas.Any(le => le.Email == empresa.Email);
            var cnpjExistente = _context.LoginEmpresas.Any(le => le.CNPJ == empresa.CNPJ);

            if (emailExistente) {
                TempData["MensagemErro"] = $"O e-mail já existe. Por favor, use outro e-mail.";
                return View(empresa);
            }

            if (cnpjExistente) {
                TempData["MensagemErro"] = $"O CNPJ já existe. Por favor, use outro e-mail.";
                return View(empresa);
            }


            try {

                empresa.Senha = HashSenha(empresa.Senha);
                // Adiciona a empresa ao banco de dados
                _context.Empresas.Add(empresa);
                _context.SaveChanges();

                // Cria o registro correspondente na tabela LoginEmpresa
                var loginEmpresa = new LoginEmpresaModel {
                    Email = empresa.Email,
                    Password = empresa.Senha,
                    ID_EMPRESA = empresa.Id,
                    CNPJ = empresa.CNPJ,
                    RazaoSocial = empresa.RazaoSocial,
                    ResetCode = "" // Valor inicial para evitar erro de nulidade
                };

                _context.LoginEmpresas.Add(loginEmpresa);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = $"Cadastro criado com sucesso. Faça seu login!";
                return RedirectToAction("LoginEmpresa", "Login");  
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                TempData["MensagemErro"] = $"Ocorreu um erro, tente novamente!";
                return View(empresa);
            }
        }
        return View(empresa);
    }


    public IActionResult EmpresaLogado() {
        var estudantes = _context.Estudantes
            .Include(e => e.Curriculo)
            .ToList();

        var model = estudantes.Select(e => new EmpresaEstudanteViewModel {
            Estudante = e,
            Curriculo = e.Curriculo
        }).ToList();

        return View(model);
    }

    public IActionResult ListarEstudantes() {
        var estudantes = _context.Estudantes.ToList();

        return View(estudantes);
    }

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
                TempData["MensagemErro"] = $"ConfirmarSenha: A senha e a confirmação de senha não correspondem.";
                return View(estudante);
            }

            // Verifica se o email ou CPF já está cadastrado
            var emailExistente = _context.Estudantes.Any(e => e.Email == estudante.Email);
            var cpfExistente = _context.Estudantes.Any(e => e.CPF == estudante.CPF);

            if (emailExistente) {
                TempData["MensagemErro"] = $"Email: O e-mail já existe. Por favor, use outro e-mail.";
                return View(estudante);
            }

            if (cpfExistente) {
                TempData["MensagemErro"] = $"CPF: O CPF já existe. Por favor, use outro CPF.";
                return View(estudante);
            }

            try {
                // Hash da senha
                estudante.Senha = HashSenha(estudante.Senha);

                _context.Estudantes.Add(estudante);
                _context.SaveChanges();

                var loginEstudante = new LoginEstudanteModel {
                    Email = estudante.Email,
                    Senha = estudante.Senha,
                    ID_ESTUDANTE = estudante.Id,
                    ResetCode = "" // Valor inicial para evitar erro de nulidade
                };
                _context.LoginEstudantes.Add(loginEstudante);
                _context.SaveChanges();

                var curriculo = new CurriculoModel {
                    NomeCompleto = estudante.Nome,
                    Email = estudante.Email,
                    DataNascimento = estudante.DataNascimento,
                    Telefone = estudante.Telefone,
                    ID_ESTUDANTE = estudante.Id


                };
                _context.Curriculo.Add(curriculo);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = $"Cadastro criado com sucesso. Faça seu login!";
                return RedirectToAction("LoginEstudante", "Login");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                TempData["MensagemErro"] = $"Ocorreu um erro, tente novamente!";
                return View(estudante);
            }
        }
        return View(estudante);
    }


    // Método para hashear a senha usando SHA256 e codificá-la em Base64
    private string HashSenha(string senha) {
        using (SHA256 sha256 = SHA256.Create()) {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToBase64String(bytes);
        }
    }

    public IActionResult CadastrarVagas(int id) {
        var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
        if (empresa == null) {
            return NotFound();
        }

        var model = new CadastrarVagasViewModel {
            CadastrarVagas = new CadastrarVagasModel { ID_EMPRESA = empresa.Id }, // Define o EmpresaId
            Empresas = _context.Empresas.ToList(), // Preencher a lista de empresas, se necessário
            RazaoSocial = empresa.RazaoSocial

        };

        return View(model);
    }






    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CadastrarVagas(CadastrarVagasViewModel model) {
        var vaga = model.CadastrarVagas; // Acesse o modelo CadastrarVagas a partir do view model

        var empresaExiste = await _context.Empresas.AnyAsync(e => e.Id == vaga.ID_EMPRESA);
        if (!empresaExiste) {
            ModelState.AddModelError("EmpresaId", "A empresa associada à vaga não existe.");

            // Preenche a lista de empresas novamente para mostrar na view
            model.Empresas = await _context.Empresas.ToListAsync();
            return View(model); 
        }
        // Adiciona a nova vaga
        _context.Vagas.Add(vaga);
        await _context.SaveChangesAsync();
        TempData["MensagemSucesso"] = $"Vaga cadastrada com sucesso!";
        return RedirectToAction("MinhasVagasView");
    }

    public IActionResult MinhasVagasView() {
        var empresaIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (empresaIdClaim == null) {
            TempData["MensagemErro"] = "ID da empresa não encontrado.";
            return RedirectToAction("EmpresaLogado", "Cadastro");
        }

        // Tenta converter a claim para um int
        if (!int.TryParse(empresaIdClaim.Value, out int empresaId)) {
            TempData["MensagemErro"] = "ID da empresa inválido.";
            return RedirectToAction("EmpresaLogado", "Cadastro");
        }

        // Filtra as vagas pela empresa
        var vagas = _context.Vagas.Where(v => v.ID_EMPRESA == empresaId).ToList();

        // Se não houver vagas, adiciona uma mensagem informativa
        if (!vagas.Any()) {
            ViewBag.Mensagem = "Você ainda não cadastrou nenhuma vaga.";
        }

        return View(vagas); 
    }





    [HttpPost]
    public async Task<IActionResult> AtivarDesativar(int id) {
        var vaga = await _context.Vagas.FindAsync(id);

        if (vaga == null) {
            TempData["MensagemErro"] = "Vaga não encontrada.";
            return RedirectToAction("MinhasVagasView");
        }
        // Alterna o status da vaga
        vaga.Status = !vaga.Status;

        _context.Vagas.Update(vaga);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = $"Vaga {(vaga.Status ? "ativada" : "desativada")} com sucesso!";
        return RedirectToAction("MinhasVagasView");
    }



    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id) {
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
