using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Usuarios.Interfaces
{
    public interface IUsuariosAppServico
    {
        Task<UsuarioResponse> CadastrarUsuarioAsync(UsuarioCadastroRequest request, CancellationToken ct);
        Task DeletarUsuarioAsync(int id, CancellationToken ct);
        Task<PaginacaoConsulta<UsuarioResponse>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct);
    }
}
