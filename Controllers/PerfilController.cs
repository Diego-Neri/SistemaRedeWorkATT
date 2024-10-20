using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;

namespace SistemaRedeWork.Controllers {
    public class PerfilController : Controller {

        private readonly BancoContext _context;

        public PerfilController(BancoContext context) {
            _context = context;
        }

        public IActionResult PerfilEstudante(int id) {
            var estudante = _context.Estudantes.FirstOrDefault(e => e.Id == id);
            if (estudante == null) {
                return NotFound(); // Retorna 404 se o estudante não for encontrado
            }

            var model = new EstudanteModel {
                Id = estudante.Id,
                Nome = estudante.Nome,
                Sobrenome = estudante.Sobrenome,
                CPF = estudante.CPF,
                Email = estudante.Email,
                Telefone = estudante.Telefone,
                CEP = estudante.CEP,
                Rua = estudante.Rua,
                Numero = estudante.Numero,
                Sexo = estudante.Sexo,
                Estado = estudante.Estado,
                // Adicione outros campos necessários
            };

            return View(model); // Retorne a view com o modelo preenchido

        }

        [HttpPost]
        public async Task<IActionResult> AtualizarPerfil(int id, EstudanteModel model) {
            // Busca o registro no banco de dados
            var estudante = await _context.Estudantes.FindAsync(id);

            // Verifica se o registro existe
            if (estudante == null) {
                return NotFound("Estudante não encontrado.");
            }

            // Atualiza apenas os campos que precisam ser modificados
            estudante.Nome = model.Nome;
            estudante.Sobrenome = model.Sobrenome;
            estudante.Email = model.Email;
            estudante.Telefone = model.Telefone;
            estudante.CEP = model.CEP;
            estudante.Rua = model.Rua;
            estudante.Numero = model.Numero;
            estudante.Sexo = model.Sexo;
            estudante.Estado = model.Estado;

            // Opcional: Atualizar a senha, somente se fornecida
            //if (!string.IsNullOrWhiteSpace(model.Senha)) {
            //    estudante.Senha = HashSenha(model.Senha); // Função para hashear a senha
            //}

            // Atualiza o banco de dados com os campos modificados
            _context.Estudantes.Update(estudante);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = $"Dados atualizados!";
            return RedirectToAction("PerfilEstudante", new { id = estudante.Id });
        }


        public async Task<IActionResult> Curriculo(int id) {
            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null) {
                TempData["MensagemErro"] = "Estudante não encontrado!";
                return RedirectToAction("EstudanteLogado");
            }
            var nomeCompleto = $"{estudante.Nome} {estudante.Sobrenome}";

            // Preenche os campos do Currículo com os dados do Estudante
            var viewModel = new CurriculoViewModel {
                Curriculo = new CurriculoModel {
                    NomeCompleto = nomeCompleto, // Preenchendo automaticamente
                    Email = estudante.Email,
                    DataNascimento = estudante.DataNascimento,
                    Telefone = estudante.Telefone,
                    Universidade = estudante.Instituicao,
                    Curso = estudante.Curso,
                    Semestre = estudante.Semestre
                },
                Estudante = estudante,
                Arquivos = _context.Arquivos.ToList()
            };

            return View(viewModel);
        }




        [HttpPost]
        public async Task<IActionResult> SalvarCurriculo(int id, CurriculoViewModel viewModel) {
            // Verifica se o estado do modelo é inválido
            if (!ModelState.IsValid) {
                foreach (var entry in ModelState) {
                    foreach (var error in entry.Value.Errors) {
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }
                TempData["MensagemErro"] = "Não foi possível salvar o seu currículo!";
                return View("Curriculo", viewModel);  // Retorna a view com os erros
            }

            // Verifica se estamos editando ou criando um novo currículo
            CurriculoModel curriculo;

            if (id == 0) {
                // Novo currículo
                curriculo = new CurriculoModel();
                _context.Curriculo.Add(curriculo);  // Adiciona o novo currículo ao contexto
            } else {
                // Editando um currículo existente
                curriculo = await _context.Curriculo.FindAsync(id);
                if (curriculo == null) {
                    TempData["MensagemErro"] = "Currículo não encontrado!";
                    return RedirectToAction("Curriculo");  // Redireciona se o currículo não for encontrado
                }
            }

            // Atualiza os dados do currículo a partir do ViewModel
            curriculo.NomeCompleto = viewModel.Curriculo.NomeCompleto;
            curriculo.Email = viewModel.Curriculo.Email;
            curriculo.DataNascimento = viewModel.Curriculo.DataNascimento;
            curriculo.Telefone = viewModel.Curriculo.Telefone;
            curriculo.Objetivo = viewModel.Curriculo.Objetivo;
            curriculo.Universidade = viewModel.Curriculo.Universidade;
            curriculo.Curso = viewModel.Curriculo.Curso;
            curriculo.Semestre = viewModel.Curriculo.Semestre;
            curriculo.Educacao = viewModel.Curriculo.Educacao;
            curriculo.Experiencia = viewModel.Curriculo.Experiencia;
            curriculo.Habilidade = viewModel.Curriculo.Habilidade;
            curriculo.Certificado = viewModel.Curriculo.Certificado;
            curriculo.Idioma = viewModel.Curriculo.Idioma;

            // Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = "Currículo salvo com sucesso!";
            return RedirectToAction("Curriculo");
        }


        [Authorize]
        [HttpPost]
        public IActionResult UploadImagem(IList<IFormFile> arquivos) {
            IFormFile imagemCarregada = arquivos.FirstOrDefault();

            if (imagemCarregada != null) {
                MemoryStream ms = new MemoryStream();
                imagemCarregada.OpenReadStream().CopyTo(ms);

                Arquivos arqui = new Arquivos() {
                    Descricao = imagemCarregada.FileName,
                    Dados = ms.ToArray(),
                    ContentType = imagemCarregada.ContentType
                };

                _context.Arquivos.Add(arqui);
                _context.SaveChanges();
            }
            TempData["MensagemSucesso"] = $"Dados salvos com sucesso!";
            return RedirectToAction("EstudanteLogado");
        }

        public IActionResult Visualizar(int id) {
            var arquivosBanco = _context.Arquivos.FirstOrDefault(a => a.Id == id);

            return File(arquivosBanco.Dados, arquivosBanco.ContentType);
        }
    }
}
