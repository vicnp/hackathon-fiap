using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;

namespace Hackathon.Fiap.Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        PaginacaoConsulta<Usuario> ListarUsuarios(UsuarioListarRequest request);
        Usuario RecuperarUsuario(string email, string hash);
    }
}
