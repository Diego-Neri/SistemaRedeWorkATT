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

        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() {
            return View();
        }
        [HttpGet]
        [Route("ForgotPasswordEmpresa")]
        public IActionResult ForgotPasswordEmpresa() {
            return View();
        }


        [HttpPost]
        [Route("ForgotPassword")]
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



        //MÉTODO EMPRESA
        [HttpPost]
        [Route("ForgotPasswordEmpresa")]
        public IActionResult ForgotPasswordEmpresa(string email) {
            var user = _context.LoginEmpresas.FirstOrDefault(u => u.Email == email);

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

            return View("ValidateResetCodeEmpresa");
        }




        public void SendResetEmail(string toEmail, string resetCode) {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587) {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("diegotrampo500@gmail.com", "ytpu uanf bqli icde"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage {
                From = new MailAddress("diegotrampo500@gmail.com"),
                Subject = "RedeWork - Redefinicão de senha",
                Body = $@"
                                            <html>
                                              <body style='font-family: Arial, sans-serif; background-color: #f5f5f5; margin: 0; padding: 0;'>
                                                <table width='100%' cellpadding='0' cellspacing='0' style='margin: 0; padding: 20px 0;'>
                                                  <tr>
                                                    <td align='center'>
                                                      <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 10px; box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); overflow: hidden;'>
                                                        <!-- Header -->
                                                        <tr>
                                                          <td style='background: linear-gradient(to right, #a17bc4, #5D2CE7); padding: 5px; text-align: center;'>
                                                             <img src='https://i.imgur.com/uscpPaC.png' alt='RedeWork Logo' style='max-width: 200px;'>
                                                           </td>
                                                        </tr>

                                                        <!-- Body -->
                                                        <tr>
                                                          <td style='padding: 30px;'>
                                                            <h1 style='font-size: 24px; color: #333333; margin-bottom: 20px; text-align: center;'>Solicitação de Redefinição de Senha</h1>
                
                                                            <p style='font-size: 16px; color: #333333; line-height: 1.6;'>
                                                              Olá,
                                                            </p>
                                                            <p style='font-size: 16px; color: #333333; line-height: 1.6;'>
                                                              Recebemos uma solicitação para redefinir sua senha. Se você não fez essa solicitação, entre em contato imediatamente com nosso suporte técnico.
                                                            </p>
                                                            <p style='font-size: 16px; color: #333333; line-height: 1.6;'>
                                                              Sua nova senha é:
                                                            </p>
                
                                                            <div style='text-align: center; margin: 20px 0;'>
                                                              <span style='font-size: 24px; color: #ffffff; background-color: #D9534F; padding: 15px 30px; border-radius: 5px; display: inline-block; font-weight: bold;'>
                                                                {resetCode}
                                                              </span>
                                                            </div>
                
                                                            <p style='font-size: 16px; color: #333333; line-height: 1.6;'>
                                                              Caso precise de mais assistência, nossa equipe de suporte está disponível para ajudá-lo. Entre em contato conosco pelo e-mail abaixo:
                                                            </p>

                                                            <p style='font-size: 18px; text-align: center;'>
                                                              <a href='mailto:suporte@dominio.com' style='color: #007BFF; font-weight: bold; text-decoration: none;'>suporte@dominio.com</a>
                                                            </p>
                
                                                            <hr style='border: none; border-top: 1px solid #dddddd; margin: 30px 0;' />

                                                            <p style='font-size: 14px; color: #888888; text-align: center;'>
                                                              Este é um e-mail automatizado enviado por <strong>RedeWork</strong>. Por favor, não responda a este e-mail.
                                                            </p>
                                                          </td>
                                                        </tr>

                                                        <!-- Footer -->
                                                        <tr>
                                                          <td style='background-color: #f7f7f7; padding: 20px; text-align: center;'>
                                                            <p style='font-size: 14px; color: #888888;'>
                                                              © 2024 RedeWork. Todos os direitos reservados.
                                                            </p>
                                                            <p style='font-size: 14px; color: #888888;'>
                                                              Rua Exemplo, 123 - São Paulo - SP
                                                            </p>
                                                          </td>
                                                        </tr>
                                                      </table>
                                                    </td>
                                                  </tr>
                                                </table>
                                              </body>
                                            </html>",

                IsBodyHtml = true // Usar HTML no corpo do e-mail
            };

            mailMessage.To.Add(toEmail);
            smtpClient.Send(mailMessage);
        }


        //MÉTODO ESTUDANTE
        [HttpGet]
        [Route("ResetPassword")]
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
        
        //MÉTODO EMPRESA 
        [HttpGet]
        [Route("ResetPasswordEmpresa")]
        public ActionResult ResetPasswordEmpresa(string email) {
            // Verificar se o token é válido e não expirou
            var user = _context.LoginEmpresas.FirstOrDefault(u => u.Email == email);
            if (user == null) {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return RedirectToAction("Error"); // Redirecionar para uma página de erro, se necessário
            }

            var model = new ResetPasswordViewModel { Email = email };
            return View(model);
        }


        //MÉTODO ESTUDANTE
        [HttpPost]
        [Route("ResetPassword")]
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



        //MÉTODO EMPRESA
        [HttpPost]
        [Route("ResetPasswordEmpresa")]
        public IActionResult ResetPasswordEmpresa(ResetPasswordViewModel model) {
            //  ver se que o email está sendo passado corretamente
            var user = _context.LoginEmpresas. FirstOrDefault(u => u.Email == model.Email);

            if (user == null) {
                TempData["MensagemErro"] = "Usuário não encontrado.";
                return View(); // Certifique-se de que você está retornando a view correta
            }

            if (string.IsNullOrEmpty(user.ResetCode)) {
                TempData["MensagemErro"] = "Código de redefinição não encontrado.";
                return View(); // Retorne a view correta
            }

            user.Password = HashPassword(model.NewPassword); // criptografa a nova senha
            //user.ResetCode = null; // Limpar o código após a redefinição
            user.ResetCodeExpiration = null; // Limpar a expiração do código

            try {
                _context.SaveChanges(); // Tente salvar as alterações no banco de dados
            } catch (DbUpdateException ex) {
                TempData["MensagemErro"] = "Erro ao salvar as alterações. Tente novamente.";
                // Aqui você pode registrar o erro se precisar
                return View("ResetPasswordEmpresa"); // Retorne a view correta
            }

            return RedirectToAction("LoginEmpresa", "Login"); // Redirecionar para a página de login
        }

        //Método para criptografar essa bosta
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



        //MÉTODO ESTUDANTE
        [HttpPost]
        [Route("ValidateResetCode")]
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



        //MÉTODO EMPRESA
        [HttpPost]
        [Route("ValidateResetCodeEmpresa")]
        public IActionResult ValidateResetCodeEmpresa(ResetCodeViewModel model, string email) {
            var user = _context.LoginEmpresas.FirstOrDefault(u => u.Email == model.Email);
            if (user == null) {
                TempData["MensagemErro"] = $"Usuário não encontrado.";
                return View("ValidateResetCode");
            }

            if (user == null || user.ResetCode != model.ResetCode || user.ResetCodeExpiration < DateTime.Now) {
                TempData["MensagemErro"] = "Código inválido ou expirado.";
                return View("ValidateResetCode");
            }

            // Código válido, redirecionar para a página de redefinição de senha
            return RedirectToAction("ResetPasswordEmpresa", new { email = model.Email });
        }


        [HttpGet]
        [Route("ValidateResetCode")]
        public IActionResult ValidateResetCode() {
            return View(); 
        }

        [HttpGet]
        [Route("ValidateResetCodeEmpresa")]
        public IActionResult ValidateResetCodeEmpresa() {
            return View(); 
        }



    }



}




