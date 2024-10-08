using Microsoft.AspNetCore.Mvc;

namespace SistemaRedeWork.Controllers {
    public class VagaController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult VagaCompleta() {
            return View();
        }
    }
}
