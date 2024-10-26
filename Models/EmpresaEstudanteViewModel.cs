namespace SistemaRedeWork.Models {
    public class EmpresaEstudanteViewModel {
        public EstudanteModel Estudante { get; set; }
        public EmpresaModel Empresa { get; set; }
        public List<LoginEstudanteModel> LoginEstudante { get; set; } 

        public LoginEmpresaModel LoginEmpresa { get; set; }

        public CurriculoModel? Curriculo { get; set; }
        public List<CadastrarVagasModel> Vagas { get; set; }

        public int EmpresaId { get; set; } // Ou string, dependendo do tipo de ID
        public int EstudanteId { get; set; } // Ou string, dependendo do tipo de ID


    }
}
