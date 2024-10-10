using SistemaRedeWork.Models;


namespace SistemaRedeWork.Helper {
    public interface ISessao {
        void CriarSessaoDoUsuario(UsuarioModel usuario);

        void RemoverSessaoDoUsuario();

        UsuarioModel BuscarSessaoDoUsuario();
    }
}
