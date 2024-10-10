﻿using System.ComponentModel.DataAnnotations;

namespace SistemaRedeWork.Models {
    public class LoginEstudanteModel {
        [Required(ErrorMessage = "Digite o login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }
    }
}

