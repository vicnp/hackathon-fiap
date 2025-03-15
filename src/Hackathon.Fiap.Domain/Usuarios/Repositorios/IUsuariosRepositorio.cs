using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;

namespace Hackathon.Fiap.Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task<PaginacaoConsulta<Usuario>> ListarUsuariosAsync(UsuarioListarRequest request, CancellationToken ct);
        Task<Usuario?> RecuperarUsuarioAsync(string email, string hash, CancellationToken ct);
    }
}
