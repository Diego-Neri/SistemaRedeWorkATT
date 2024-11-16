using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {


    [Table("VAGAS")]
    public class CadastrarVagasModel {
        [Key]
        [Column("ID_VAGA")]
        public int Id { get; set; }

        //[Required]
        [StringLength(100)]
        [Column("TITULO")]
        public string Titulo { get; set; }

        

        //[Required]
        [StringLength(1000)]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        
        [StringLength(500)]
        [Column("REQUISITOS")]
        public string Requisitos { get; set; }


        // Tipo de trabalho (ex: Remoto, Presencial, Híbrido)
        //[Required]
        [StringLength(50)]
        [Column("TIPO_TRABALHO")]
        public string TipoTrabalho { get; set; }


        // Nível de experiência (ex: Júnior, Pleno, Sênior)
        //[Required]
        [StringLength(50)]
        [Column("NIVEL_EXPERIENCIA")]
        public string NivelExperiencia { get; set; }

        // Faixa salarial mínima
        //[Range(0, double.MaxValue, ErrorMessage = "A faixa salarial mínima deve ser maior ou igual a zero.")]
        [Column("MIN_SALARIAL")]
        public decimal FaixaSalarialMin { get; set; }

        // Faixa salarial máxima
        //[Range(0, double.MaxValue, ErrorMessage = "A faixa salarial máxima deve ser maior ou igual a zero.")]
        [Column("MAX_SALARIAL")]
        public decimal FaixaSalarialMax { get; set; }

        // Localização da vaga
        [StringLength(100)]
        [Column("LOCALIZACAO")]
        public string Localizacao { get; set; }

        // Duração do projeto (ex: Curto prazo, Longo prazo)
        //[Required]
        [StringLength(50)]
        [Column("DURACAO_PROJETO")]
        public string DuracaoProjeto { get; set; }

        // Modalidade de contratação (ex: PJ, CLT, Freelancer)
        //[Required]
        [StringLength(50)]
        [Column("MODALIDADE_CONTRATO")]
        public string ModalidadeContratacao { get; set; }

        // Benefícios oferecidos
        [StringLength(500)]
        [Column("BENEFICIOS")]
        public string Beneficios { get; set; }

        // Data limite para candidatura
        [Column("DATA_LIMITE")]
        public DateTime DataLimiteCandidatura { get; set; } = DateTime.Now.AddDays(30); // Valor padrão de 30 dias a partir de agora


        // ID da empresa associada à vaga
        [Column("ID_EMPRESA")]
        public int ID_EMPRESA { get; set; }

        // Propriedade de navegação para a empresa associada à vaga
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
