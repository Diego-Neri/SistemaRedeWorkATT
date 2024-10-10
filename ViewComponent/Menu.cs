using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SistemaRedeWork.Models;
using System.Threading.Tasks;

namespace SistemaRedeWork.ViewComponents {
    public class Menu : ViewComponent {
        public async Task<IViewComponentResult> InvokeAsync() {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            UsuarioModel usuario = null;
            if (!string.IsNullOrEmpty(sessaoUsuario)) {
                usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
            }

            // Retorna a view com o modelo de usuário (ou null se não houver usuário)
            return View(usuario); // Aqui, se usuario for null, a view deve lidar com isso.
        }
    }
}
