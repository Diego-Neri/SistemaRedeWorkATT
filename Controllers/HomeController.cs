using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using System.Diagnostics;

namespace SistemaRedeWork.Controllers {
    public class HomeController : Controller {
        private readonly BancoContext _context;

        public HomeController(BancoContext context) {
            _context = context;
        }

        public IActionResult Index() {
            //var usuarios = _context.Usuarios.ToList(); // "Usuarios" � o DbSet na classe BancoContext

            //if (usuarios != null) {
            //    ViewData["Message"] = $"Conex�o bem-sucedida! N�mero de usu�rios: {usuarios.Count}";
            //} else {
            //    ViewData["Message"] = "Falha na conex�o com o banco de dados.";
            //}
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
