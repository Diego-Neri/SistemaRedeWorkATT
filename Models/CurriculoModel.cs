using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {

    [Table("CURRICULO")]
    public class CurriculoModel {

        [Key]
        [Column("ID_CURRICULO")]
        public int Id { get; set; }

        [StringLength(255)]
        [Column("NOME")]
        public string NomeCompleto { get; set; }

        [StringLength(255)]
        [Column("EMAIL")]
        public string Email { get; set; }


        [Column("DATA_NASC")]
        public DateTime? DataNascimento { get; set; }

        [Column("TELEFONE")]
        public string Telefone { get; set; }

        [StringLength(500)]
        [Column("OBJETIVO")]
        public string? Objetivo { get; set; }

        [StringLength(255)]
        [Column("UNIVERSIDADE")]
        public string? Universidade { get; set; }

        [StringLength(255)]
        [Column("CURSO")]
        public string? Curso { get; set; }

        [StringLength(255)]
        [Column("SEMESTRE")]
        public string? Semestre { get; set; }


        [Column("PERIODO")]
        public string? Periodo { get; set; }

        [StringLength(255)]
        [Column("EDUCACAO")]
        public string? Educacao { get; set; }

        [StringLength(255)]
        [Column("EXPERIENCIA")]
        public string? Experiencia { get; set; }

        [StringLength(255)]
        [Column("HABILIDADE")]
        public string? Habilidade { get; set; }

        //[Column("NOME")]
        //public string Certificado { get; set; }

        [StringLength(255)]
        [Column("IDIOMA")]
        public string? Idioma { get; set; }

        public int ID_ESTUDANTE { get; set; } // Chave estrangeira

        [ForeignKey("ID_ESTUDANTE")]
        public EstudanteModel? Estudante { get; set; } // Propriedade de navegação

    }

    [Table("ARQUIVOS")]
    public class ArquivoModel {

        [Column("ID_CURRICULO")]
        public int Id { get; set; }

        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("DADOS")]
        public byte[] Dados { get; set; }

        public string ContentType { get; set; }

        [ForeignKey("ID_ESTUDANTE")]
        public int ID_ESTUDANTE { get; set; } // Chave estrangeira obrigatória
        public EstudanteModel Estudante { get; set; } // Propriedade de navegação
    }

}
