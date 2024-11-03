namespace SistemaRedeWork.Models {
    public class CandidaturaModel {
        public int Id { get; set; }
        public int VagaId { get; set; }
        public CadastrarVagasModel Vagas { get; set; }
        public int EstudanteId {  get; set; }
        public EstudanteModel Estudante { get; set; }
        public DateTime DataCandidatura { get; set; }
    }
}
