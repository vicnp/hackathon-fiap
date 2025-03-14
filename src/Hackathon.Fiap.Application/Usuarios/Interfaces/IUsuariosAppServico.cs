using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;

namespace Hackathon.Fiap.Application.Usuarios.Interfaces
{
    public interface IUsuariosAppServico
    {
        PaginacaoConsulta<UsuarioResponse> ListarPacientes(UsuarioListarRequest request);
    }
}
