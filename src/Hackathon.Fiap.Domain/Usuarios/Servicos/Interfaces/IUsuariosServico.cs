using Hackathon.Fiap.Domain.Usuarios.Comandos;
using Hackathon.Fiap.Domain.Usuarios.Entidades;

namespace Hackathon.Fiap.Domain.Usuarios.Servicos.Interfaces
{
    public interface IUsuariosServico
    {
        Task<Usuario> CadastrarUsuarioAsync(UsuarioCadastroComando comando, CancellationToken ct);
    }
}
