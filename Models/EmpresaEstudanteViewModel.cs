namespace SistemaRedeWork.Models {
    public class EmpresaEstudanteViewModel {
        public EstudanteModel Estudante { get; set; }
        public EmpresaModel Empresa { get; set; }
        public List<LoginEstudanteModel> LoginEstudante { get; set; } 

        public LoginEmpresaModel LoginEmpresa { get; set; }

        public CurriculoModel? Curriculo { get; set; }
        public List<CadastrarVagasModel> Vagas { get; set; }

        public int ID_EMPRESA { get; set; }
        public int ID_ESTUDANTE { get; set; } 


    }
}
