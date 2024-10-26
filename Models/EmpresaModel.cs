using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SistemaRedeWork.Models {
    public class EmpresaModel {
        [Key]
        public int Id { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }


        //[Required]
        public string Usuario { get; set; }

        //[Required]
        public string RazaoSocial { get; set; }

        //[Required]
        //[RegularExpression(@"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$", ErrorMessage = "O CNPJ deve estar no formato 00.000.000/0000-00.")]
        public string CNPJ { get; set; }

        //[Phone]
        public string Telefone { get; set; }

        public string Site { get; set; }

        public string Linkedin { get; set; }

        //[Required]
        public string Estado { get; set; }

        //[Required]
        public string Cidade { get; set; }

        //[Required]
        //[RegularExpression(@"\d{5}-\d{3}", ErrorMessage = "O CEP deve estar no formato 00000-000")]
        public string CEP { get; set; }

        //[Required]
        public string Rua { get; set; }

        public string Numero { get; set; }
        //[Required]
        //[DataType(DataType.Password)]
        public string Senha { get; set; }

        //[Required(ErrorMessage = "A confirmação da senha é obrigatória.")]
        public string ConfirmarSenha { get; set; }
        public List<EstudanteModel>? Estudantes { get; set; }
        public List<CadastrarVagasModel>? Vagas { get; set; } = new List<CadastrarVagasModel>();
    }

    public class LoginEmpresaModel {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        public string Password { get; set; }

        public string? CNPJ { get; set; }
        public string? RazaoSocial { get; set; }

        // Chave estrangeira para vincular com a Empresa
        [ForeignKey("EmpresaModel")]
        public int? EmpresaId { get; set; }

        public EmpresaModel? Empresa { get; set; }

        public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpiration { get; set; }
        public bool RememberMe { get; set; } // Adiciona a propriedade RememberMe
    }


    
}
