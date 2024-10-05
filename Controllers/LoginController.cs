using Microsoft.AspNetCore.Mvc;

namespace SistemaRedeWork.Controllers {
    public class LoginController : Controller {
        public IActionResult LoginEstudante() {
            return View();
        }
        
        public IActionResult LoginEmpresa() {
            return View();
        }
    }
}
