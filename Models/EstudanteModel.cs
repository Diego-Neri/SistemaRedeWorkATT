using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {
    public class EstudanteModel {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string CEP { get; set; }

        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Sexo { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Instituicao { get; set; }

        public string Periodo { get; set; }

        public string Curso { get; set; }

        public string Semestre { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }


    }
    public class LoginEstudanteModel {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o login")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }

        [ForeignKey("EstudanteModel")]
        public int EstudanteId { get; set; }

        public bool RememberMe { get; set; } // Adiciona a propriedade RememberMe

    }
}
