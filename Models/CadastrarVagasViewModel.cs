using SistemaRedeWork.Models;

public class CadastrarVagasViewModel {
    public CadastrarVagasModel CadastrarVagas { get; set; }
    public EmpresaEstudanteViewModel EmpresaEstudante { get; set; }
    public List<EmpresaModel> Empresas { get; set; } 
    public string RazaoSocial { get; set; } 

}
