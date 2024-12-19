using System.ComponentModel.DataAnnotations;

namespace SistemaRedeWork.Models {
    public class ForgotPasswordViewModel {

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel {
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a nova senha")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetCodeViewModel {
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o código")]
        public string ResetCode { get; set; }
    }


}
