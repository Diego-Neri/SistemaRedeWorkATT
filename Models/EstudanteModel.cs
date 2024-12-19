using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaRedeWork.Models {

    [Table("ESTUDANTE")]
    public class EstudanteModel {
        [Key]
        [Column("ID_ESTUDANTE")]
        public int Id { get; set; }

        [Column("NOME")]
        [StringLength(255)]
        public string Nome { get; set; }

        [Column("CPF")]
        [StringLength(255)]
        public string CPF { get; set; }

        [Column("TELEFONE")]
        [StringLength(255)]
        public string Telefone { get; set; }

        [Column("EMAIL")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("CEP")]
        [StringLength(255)]
        public string CEP { get; set; }

        [Column("LOGRADOURO")]
        [StringLength(255)]
        public string Rua { get; set; }


        [Column("NUMERO")]
        [StringLength(255)]
        public string Numero { get; set; }

        [Column("GENERO")]
        [StringLength(255)]
        public string Sexo { get; set; }

        [Column("ESTADO")]
        [StringLength(255)]
        public string Estado { get; set; }

        [Column("CIDADE")]
        [StringLength(255)]
        public string Cidade { get; set; }


        [Column("DATA_NASC")]
        public DateTime? DataNascimento { get; set; }


        [Column("SENHA")]
        public string Senha { get; set; }

        [Column("CONFIRMAR_SENHA")]
        public string ConfirmarSenha { get; set; }

        public static implicit operator List<object>(EstudanteModel v) {
            throw new NotImplementedException();
        }

        // Propriedade de navegação para varios arquivos 
        public ICollection<ArquivoModel>? Arquivos { get; set; }

        [ForeignKey("Curriculo")]
        public int ID_CURRICULO { get; set; }
        public CurriculoModel? Curriculo { get; set; }
    }


    [Table("LOGINESTUDANTES")]
    public class LoginEstudanteModel {

        [Key]
        [Column("ID_USUARIO")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o login")]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        [Column("SENHA")]
        public string Senha { get; set; }

        public string? Nome => Estudante?.Nome;

        [ForeignKey("Estudante")]   
        public int? ID_ESTUDANTE { get; set; }
        public virtual EstudanteModel? Estudante { get; set; } // Corrigido para EstudanteModel

        [Column("RESET_CODE")]
        public string? ResetCode { get; set; }

        [Column("RESET_CODE_EXPIRATION")]
        public DateTime? ResetCodeExpiration { get; set; }

        [Column("REMEMBER_ME")]
        public bool RememberMe { get; set; }

    }
}
