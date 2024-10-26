using SistemaRedeWork.Models;

public class CadastrarVagasViewModel {
    public CadastrarVagasModel CadastrarVagas { get; set; }
    public EmpresaEstudanteViewModel EmpresaEstudante { get; set; }
    public List<EmpresaModel> Empresas { get; set; } // Exemplo de lista de empresas, se necessário
    public string RazaoSocial { get; set; } // Adiciona a propriedade para a razão social

}
