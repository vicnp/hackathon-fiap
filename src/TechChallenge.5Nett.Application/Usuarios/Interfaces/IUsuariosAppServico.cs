using Utils;
using Usuarios.Response;
using Usuarios.Request;

namespace Usuarios.Interfaces
{
    public interface IUsuariosAppServico
    {
        PaginacaoConsulta<UsuarioResponse> ListarPacientes(UsuarioListarRequest request);
    }
}
