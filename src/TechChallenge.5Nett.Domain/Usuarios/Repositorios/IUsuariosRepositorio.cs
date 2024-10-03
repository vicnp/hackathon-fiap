using Usuarios.Entidades;
using Usuarios.Request;
using Utils;

namespace Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request);
        Usuario RecuperarUsuario(string email, string hash);
    }
}
