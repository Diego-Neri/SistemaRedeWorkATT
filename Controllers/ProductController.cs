using Microsoft.AspNetCore.Mvc;
using SistemaRedeWork.Data;
using SistemaRedeWork.Models;
using System.Threading.Tasks;

namespace SistemaRedeWork.Controllers {
    public class ProductController : Controller {
        private readonly BancoContext _context;

        public ProductController(BancoContext context) {
            _context = context;
        }

        // Exibe o formulário de inserção
        public IActionResult Inserir() {
            return View();
        }

        // Insere um novo produto no banco de dados
        [HttpPost]
        public async Task<IActionResult> Inserir(ProductModel produto) {
            if (ModelState.IsValid) {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(produto);
        }
    }
}
