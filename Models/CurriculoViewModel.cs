using SistemaRedeWork.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CurriculoViewModel {
    public CurriculoModel Curriculo { get; set; }
    public List<ArquivoModel>? Arquivos { get; set; }
    public EmpresaModel? Empresa { get; set; }
    public EstudanteModel? Estudante { get; set; }
}
