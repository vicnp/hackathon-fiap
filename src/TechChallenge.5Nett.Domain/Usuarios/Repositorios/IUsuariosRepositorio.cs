using Usuarios.Entidades;
using Usuarios.Request;
using Utils;

namespace Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        PaginacaoConsulta<Usuario> ListarPacientes(PacienteListarRequest request);
        PaginacaoConsulta<Usuario> ListarUsuarios(PacienteListarRequest request);
        Usuario RecuperarUsuario(string email, string hash);
    }
}
