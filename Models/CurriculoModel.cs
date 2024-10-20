using System.ComponentModel.DataAnnotations;

namespace SistemaRedeWork.Models {
    public class CurriculoModel {
        [Key]
        public int Id { get; set; }

        public string NomeCompleto { get; set; }
        public string Email { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Telefone { get; set; }

        public string Objetivo { get; set; }

        public string Universidade { get; set; }

        public string Curso { get; set; }

        public string Semestre { get; set; }
        public string Educacao { get; set; }

        public string Experiencia { get; set; }
        public string Habilidade { get; set; }

        public string Certificado { get; set; }

        public string Idioma { get; set; }


    }

    public class Arquivos {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public byte[] Dados { get; set; }
        public string ContentType { get; set; }
    }
}
