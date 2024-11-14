using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaRedeWork.Models {

    [Table("CANDIDATURA")]
    public class CandidaturaModel {

        [Key]
        [Column("ID_CANDIDATURA")]
        public int Id { get; set; }


        [Column("ID_VAGA")]
        [ForeignKey("Vagas")]
        public int VagaId { get; set; }


        public CadastrarVagasModel Vagas { get; set; }

        [Column("ID_ESTUDANTE")]
        [ForeignKey("Estudante")]
        public int EstudanteId {  get; set; }
        public EstudanteModel Estudante { get; set; }


        [Column("DATA_CANDIDATURA")]
        public DateTime DataCandidatura { get; set; }
    }
}
