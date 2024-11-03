using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {
    public class CadastrarVagasModel {
        [Key]
        public int Id { get; set; }

        // Título da vaga
        //[Required]
        //[StringLength(100)]
        public string Titulo { get; set; }

        // Descrição da vaga
        //[Required]
        //[StringLength(1000)]
        public string Descricao { get; set; }

        // Requisitos da vaga
        //[StringLength(500)]
        public string Requisitos { get; set; }

        // Tipo de trabalho (ex: Remoto, Presencial, Híbrido)
        //[Required]
        //[StringLength(50)]
        public string TipoTrabalho { get; set; }

        // Nível de experiência (ex: Júnior, Pleno, Sênior)
        //[Required]
        //[StringLength(50)]
        public string NivelExperiencia { get; set; }

        // Faixa salarial mínima
        //[Range(0, double.MaxValue, ErrorMessage = "A faixa salarial mínima deve ser maior ou igual a zero.")]
        public decimal FaixaSalarialMin { get; set; }

        // Faixa salarial máxima
        //[Range(0, double.MaxValue, ErrorMessage = "A faixa salarial máxima deve ser maior ou igual a zero.")]
        public decimal FaixaSalarialMax { get; set; }

        // Localização da vaga
        //[StringLength(100)]
        public string Localizacao { get; set; }

        // Duração do projeto (ex: Curto prazo, Longo prazo)
        //[Required]
        //[StringLength(50)]
        public string DuracaoProjeto { get; set; }

        // Modalidade de contratação (ex: PJ, CLT, Freelancer)
        //[Required]
        //[StringLength(50)]
        public string ModalidadeContratacao { get; set; }

        // Benefícios oferecidos
        //[StringLength(500)]
        public string Beneficios { get; set; }

        // Data limite para candidatura
        //[DataType(DataType.Date)]
        public DateTime DataLimiteCandidatura { get; set; } = DateTime.Now.AddDays(30); // Valor padrão de 30 dias a partir de agora

        // Informações de contato
        //[Required]
        //[StringLength(100)]
        public string ContatoNome { get; set; }

        //[Required]
        //[EmailAddress]
        public string ContatoEmail { get; set; }

        //[Required]
        //[Phone]
        public string ContatoTelefone { get; set; }

        // ID da empresa associada à vaga
        //[Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public EmpresaModel Empresa { get; set; }

        // Propriedade para controlar se a vaga está ativa ou inativa
        public bool Ativa { get; set; } = true; // Valor padrão como ativa

        //public List<EstudanteModel> Estudantes { get; set; } = new List<EstudanteModel> { new EstudanteModel() };
    }
}
