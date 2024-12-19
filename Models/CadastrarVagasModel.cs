using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {


    [Table("VAGAS")]
    public class CadastrarVagasModel {
        [Key]
        [Column("ID_VAGA")]
        public int Id { get; set; }

        [StringLength(100)]
        [Column("TITULO")]
        public string Titulo { get; set; }

        [StringLength(1000)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        
        [StringLength(500)]
        [Column("REQUISITOS")]
        public string Requisitos { get; set; }

        [StringLength(50)]
        [Column("TIPO_TRABALHO")]
        public string TipoTrabalho { get; set; }

        [StringLength(50)]
        [Column("NIVEL_EXPERIENCIA")]
        public string NivelExperiencia { get; set; }

        [Column("MIN_SALARIAL")]
        public decimal FaixaSalarialMin { get; set; }

        [Column("MAX_SALARIAL")]
        public decimal FaixaSalarialMax { get; set; }

        [StringLength(100)]
        [Column("LOCALIZACAO")]
        public string Localizacao { get; set; }

        [StringLength(50)]
        [Column("DURACAO_PROJETO")]
        public string DuracaoProjeto { get; set; }

        [StringLength(50)]
        [Column("MODALIDADE_CONTRATO")]
        public string ModalidadeContratacao { get; set; }

        [StringLength(500)]
        [Column("BENEFICIOS")]
        public string Beneficios { get; set; }

        [Column("DATA_LIMITE")]
        public DateTime DataLimiteCandidatura { get; set; } = DateTime.Now.AddDays(30); // Valor padrão de 30 dias a partir de agora


        [Column("ID_EMPRESA")]
        public int ID_EMPRESA { get; set; }

        public EmpresaModel Empresa { get; set; }


        // Informações de contato da empresa (via EmpresaModel)
        public string ContatoNome => Empresa?.RazaoSocial;
        public string ContatoEmail => Empresa?.Email;
        public string ContatoTelefone => Empresa?.Telefone;

        // Propriedade para controlar se a vaga está ativa ou inativa
        [Column("STATUS")]
        public bool Status { get; set; } = true; // Valor padrão como ativa

    }
}
