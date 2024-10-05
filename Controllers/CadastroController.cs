using Microsoft.AspNetCore.Mvc;

namespace SistemaRedeWork.Controllers {
    public class CadastroController : Controller {
        public IActionResult CadastroEstudante() {
            return View();
        }
        
        public IActionResult CadastroEmpresa() {
            return View();
        }
    }
}
