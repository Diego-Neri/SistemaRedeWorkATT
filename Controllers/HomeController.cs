using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Models;
using System.Diagnostics;

namespace SistemaRedeWork.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Contato() {
            return View();
        }

        public IActionResult ComoFunciona() {
            return View();
        }
        
        public IActionResult SobreNos() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
