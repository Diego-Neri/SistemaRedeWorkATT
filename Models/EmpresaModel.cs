using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SistemaRedeWork.Models {

    [Table("EMPRESA")]
    public class EmpresaModel {
        [Key]
        [Column("ID_EMPRESA")]
        public int Id { get; set; }

        [Column("EMAIL")]
        [StringLength(255)]
        public string Email { get; set; }


        [Column("USUARIO")]
        [StringLength(255)]
        public string Usuario { get; set; }

        [Column("RAZAO_SOCIAL")]
        [StringLength(255)]
        public string RazaoSocial { get; set; }

        [Column("CNPJ")]
        [StringLength(255)]
        public string CNPJ { get; set; }

        [Column("TELEFONE")]
        [StringLength(255)]
        public string Telefone { get; set; }

        [Column("SITE")]
        [StringLength(255)]
        public string Site { get; set; }

        [Column("LINKEDIN")]
        [StringLength(255)]
        public string Linkedin { get; set; }

        [Column("ESTADO")]
        [StringLength(255)]
        public string Estado { get; set; }

        [Column("CIDADE")]
        [StringLength(255)]
        public string Cidade { get; set; }

        [Column("CEP")]
        [StringLength(255)]
        public string CEP { get; set; }

        [Column("RUA")]
        [StringLength(255)]
        public string Rua { get; set; }

        [Column("NUMERO")]
        [StringLength(255)]
        public string Numero { get; set; }

        [Column("SENHA")]
        public string Senha { get; set; }

        [Column("CONFIRMAR_SENHA")]
        public string ConfirmarSenha { get; set; }

        public List<EstudanteModel>? Estudantes { get; set; }
        public List<CadastrarVagasModel>? Vagas { get; set; } = new List<CadastrarVagasModel>();
    }

    public class LoginEmpresaModel {
        [Key]
        [Column("ID_USUARIO")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [Column("SENHA")]
        public string Password { get; set; }

        [Column("CNPJ")]
        public string? CNPJ { get; set; }

        [Column("RAZAO_SOCIAL")]
        public string? RazaoSocial { get; set; }

        [ForeignKey("Empresa")]
        public int? ID_EMPRESA { get; set; }

        public EmpresaModel? Empresa { get; set; }


        [Column("RESET_CODE")]
        public string? ResetCode { get; set; }

        [Column("RESET_CODE_EXPIRATION")]
        public DateTime? ResetCodeExpiration { get; set; }

        [Column("REMEMBER_ME")]
        public bool RememberMe { get; set; }
    }



}
