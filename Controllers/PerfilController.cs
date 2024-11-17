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

        public IActionResult PerfilEmpresa(int id) {
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);
            if (empresa == null) {
                return NotFound();
            }

            var model = new EmpresaModel {
                Id = empresa.Id,
                Usuario = empresa.Usuario,
                CNPJ = empresa.CNPJ,
                RazaoSocial = empresa.RazaoSocial,
                Email = empresa.Email,
                Telefone = empresa.Telefone,
                Site = empresa.Site,
                Linkedin = empresa.Linkedin,
                Estado = empresa.Estado,
                Cidade = empresa.Cidade,
                CEP = empresa.CEP,
                Rua = empresa.Rua,
                Numero = empresa.Numero,
            };
            return View(model);
        }


        public IActionResult PerfilEstudante(int id) {
            var estudante = _context.Estudantes.FirstOrDefault(e => e.Id == id);
            if (estudante == null) {
                return NotFound(); // Retorna 404 se o estudante não for encontrado
            }

            var model = new EstudanteModel {
                Id = estudante.Id,
                Nome = estudante.Nome,
                //Sobrenome = estudante.Sobrenome,
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
        public async Task<IActionResult> AtualizarPerfilEmpresa(int id, EmpresaModel model) {
            var empresa = await _context.Empresas.FindAsync(id);

            if (empresa == null) {
                return NotFound("Empresa não encotrada.");
            }

            empresa.Usuario = model.Usuario;
            empresa.CNPJ = model.CNPJ;
            empresa.RazaoSocial = model.RazaoSocial;
            empresa.Email = model.Email;
            empresa.Telefone = model.Telefone;
            empresa.Site = model.Site;
            empresa.Linkedin = model.Linkedin;
            empresa.Estado = model.Estado;
            empresa.Cidade = model.Cidade;
            empresa.CEP = model.CEP;
            empresa.Rua = model.Rua;
            empresa.Numero = model.Numero;

            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync();

            TempData["MensagemSucesso"] = $"Dados atualizados!";
            return RedirectToAction("PerfilEmpresa", new { id = empresa.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarPerfil(int id, EstudanteModel model) {
            // Busca o registro no banco de dados
            var estudante = await _context.Estudantes.FindAsync(id);

            // Verifica se o registro existe
            if (estudante == null) {
                return NotFound("Estudante não encontrado.");
            }

            // Verifica se o modelo é válido
            

            try {
                // Atualiza apenas os campos que podem ser modificados
                estudante.Nome = model.Nome;
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

                // Atualiza o banco de dados
                _context.Estudantes.Update(estudante);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = "Dados atualizados com sucesso!";
                return RedirectToAction("PerfilEstudante", new { id = estudante.Id });
            } catch (Exception ex) {
                // Registra o erro (opcional: log para rastreamento)
                TempData["MensagemErro"] = "Ocorreu um erro ao atualizar o perfil. Tente novamente mais tarde.";
                return RedirectToAction("PerfilEstudante", new { id });
            }
        }



        public async Task<IActionResult> Curriculo(int id) {
            var estudante = await _context.Estudantes.FindAsync(id);

            if (estudante == null) {
                TempData["MensagemErro"] = $"Estudante não encontrado!";
                return RedirectToAction("EstudanteLogado", "Login");
            }

            var nomeCompleto = $"{estudante.Nome}";

            // Busque o currículo associado ao estudante, se existir
            var curriculo = await _context.Curriculo
                .FirstOrDefaultAsync(c => c.ID_ESTUDANTE == id);

            var viewModel = new CurriculoViewModel {
                Curriculo = curriculo ?? new CurriculoModel {
                    NomeCompleto = nomeCompleto,
                    Email = estudante.Email,
                    DataNascimento = estudante.DataNascimento,
                    Telefone = estudante.Telefone,
                    //Universidade = estudante.Instituicao,
                    //Curso = estudante.Curso,
                    //Semestre = estudante.Semestre,
                    ID_ESTUDANTE = estudante.Id
                },
                Estudante = estudante,
                Arquivos = _context.Arquivos.ToList()
            };

            return View(viewModel);
        }





        [HttpPost]
        public async Task<IActionResult> SalvarCurriculo(int id, CurriculoViewModel viewModel) {
            if (!ModelState.IsValid) {
                foreach (var entry in ModelState) {
                    foreach (var error in entry.Value.Errors) {
                        Console.WriteLine($"Error in {entry.Key}: {error.ErrorMessage}");
                    }
                }
                TempData["MensagemErro"] = "Não foi possível salvar o seu currículo!";
                return View("Curriculo", viewModel);
            }

            var estudante = await _context.Estudantes.FindAsync(viewModel.Curriculo.ID_ESTUDANTE);
            if (estudante == null) {
                TempData["MensagemErro"] = "Estudante não encontrado!";
                return RedirectToAction("Curriculo");
            }

            CurriculoModel curriculo;
            if (id == 0 || viewModel.Curriculo.Id == 0) {
                // Verificar se já existe um currículo para este estudante
                curriculo = await _context.Curriculo
                    .FirstOrDefaultAsync(c => c.ID_ESTUDANTE == viewModel.Curriculo.ID_ESTUDANTE);

                if (curriculo == null) {
                    // Se não existe, cria um novo
                    curriculo = new CurriculoModel { ID_ESTUDANTE = viewModel.Curriculo.ID_ESTUDANTE };
                    _context.Curriculo.Add(curriculo);
                }
            } else {
                // Caso contrário, encontra o currículo existente
                curriculo = await _context.Curriculo.FindAsync(viewModel.Curriculo.Id);
                if (curriculo == null) {
                    TempData["MensagemErro"] = "Currículo não encontrado!";
                    return RedirectToAction("Curriculo");
                }
            }

            // Atualiza os campos do currículo
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

            // Garantir que o Periodo não seja nulo
            curriculo.Periodo = viewModel.Curriculo.Periodo ?? "Período não informado"; // Use um valor padrão se estiver nulo

            // Atualiza outros campos conforme necessário
            curriculo.Idioma = viewModel.Curriculo.Idioma;

            try {
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Currículo salvo com sucesso!";
            } catch (Exception ex) {
                TempData["MensagemErro"] = $"Erro ao salvar o currículo: {ex.Message}";
                return View("Curriculo", viewModel);
            }

            return RedirectToAction("EstudanteLogado", "Login", new { id = estudante.Id });
        }





        [Authorize]
        [HttpPost]
        public IActionResult UploadImagem(IList<IFormFile> arquivos) {
            IFormFile imagemCarregada = arquivos.FirstOrDefault();

            if (imagemCarregada != null) {
                MemoryStream ms = new MemoryStream();
                imagemCarregada.OpenReadStream().CopyTo(ms);

                ArquivoModel arqui = new ArquivoModel() {
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
