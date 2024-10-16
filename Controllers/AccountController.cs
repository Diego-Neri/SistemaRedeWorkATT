using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace SistemaRedeWork.Controllers {
    public class AccountController : Controller {

        private readonly BancoContext _context;

        public AccountController(BancoContext context) {
            _context = context;
        }
        public IActionResult ForgotPassword() {
            return View();
        }

       
        [HttpPost]
        public IActionResult ForgotPassword(string email) {
            var user = _context.LoginEstudantes.FirstOrDefault(u => u.Email == email);

            if (user == null) {
                TempData["MensagemErro"] = "E-mail não encontrado.";
                return View();
            }

            // Checa se `ResetCode` está nulo e atribui um valor padrão
            if (string.IsNullOrEmpty(user.ResetCode)) {
                user.ResetCode = GerarCodigoReset();
            }

            user.ResetCodeExpiration = DateTime.Now.AddMinutes(15);

            _context.SaveChanges();

            SendResetEmail(user.Email, user.ResetCode);

            return View("ValidateResetCode");
        }




        public void SendResetEmail(string toEmail, string resetCode) {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("diegotrampo500@gmail.com", "ytpu uanf bqli icde"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage {
                From = new MailAddress("diegotrampo500@gmail.com"),
                Subject = "Redefinição de Senha",
                Body = $"Seu código para redefinir a senha é: {resetCode}",
                IsBodyHtml = true // Usar HTML no corpo do e-mail
            };

            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
        }

        [HttpGet]
        public ActionResult ResetPassword(string email) {
            // Verificar se o token é válido e não expirou
            var user = _context.LoginEstudantes.FirstOrDefault(u => u.Email == email);
            if (user == null) {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Error"); // Redirecionar para uma página de erro, se necessário
            }

            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model) {
            // Certifique-se de que o email está sendo passado corretamente
            var user = _context.LoginEstudantes.FirstOrDefault(u => u.Email == model.Email);

            if (user == null) {
                TempData["MensagemErro"] = "Usuário não encontrado."; 
                    return View(); // Certifique-se de que você está retornando a view correta
            }

            if (string.IsNullOrEmpty(user.ResetCode)) {
                TempData["MensagemErro"] = "Código de redefinição não encontrado.";
                return View(); // Retorne a view correta
            }

            user.Senha = HashPassword(model.NewPassword); // Hash da nova senha
            //user.ResetCode = null; // Limpar o código após a redefinição
            user.ResetCodeExpiration = null; // Limpar a expiração do código

            try {
                _context.SaveChanges(); // Tente salvar as alterações no banco de dados
            } catch (DbUpdateException ex) {
                TempData["MensagemErro"] = "Erro ao salvar as alterações. Tente novamente.";
                // Aqui você pode registrar o erro se precisar
                return View("ResetPassword"); // Retorne a view correta
            }

            return RedirectToAction("LoginEstudante", "Login"); // Redirecionar para a página de login
        }


        public string HashPassword(string password) {
            using (var sha256 = SHA256.Create()) {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public string GerarCodigoReset(int length = 6) {
            Random random = new Random();
            string codigo = random.Next(0, (int)Math.Pow(10, length)).ToString("D" + length);
            return codigo;
        }
        [HttpPost]
        public IActionResult ValidateResetCode(ResetCodeViewModel model, string email) {
            var user = _context.LoginEstudantes.FirstOrDefault(u => u.Email == model.Email);
            if (user == null) {
                TempData["MensagemErro"] = $"Usuário não encontrado.";
                return View("ValidateResetCode");
            }

            if (user == null || user.ResetCode != model.ResetCode || user.ResetCodeExpiration < DateTime.Now) {
                TempData["MensagemErro"] = "Código inválido ou expirado.";
                return View("ValidateResetCode");
            }

            // Código válido, redirecionar para a página de redefinição de senha
            return RedirectToAction("ResetPassword", new { email = model.Email });
        }
        public IActionResult ValidateResetCode() { return View(); }



    }



}




