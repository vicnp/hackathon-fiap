using TC_DataTransfer.Usuarios.Request;
using TC_Domain.Usuarios.Entidades;
using TC_Domain.Utils;

namespace TC_Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request);
        Usuario RecuperarUsuario(string email, string hash);
    }
}
