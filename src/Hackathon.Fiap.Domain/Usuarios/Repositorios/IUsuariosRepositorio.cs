using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.Usuarios.Repositorios
{
    public interface IUsuariosRepositorio
    {
        Task DeletarUsuarioAsync(int id, CancellationToken ct);
        Task<Usuario> InserirUsuarioAsync(Medico novoUsuario, CancellationToken ct);
        Task<Usuario> InserirUsuarioAsync(Usuario novoUsuario, CancellationToken ct);
        Task<PaginacaoConsulta<Usuario>> ListarUsuariosAsync(UsuarioListarFiltro filtro, CancellationToken ct);
        Task<Usuario?> RecuperarUsuarioAsync(string identificador, string hash, CancellationToken ct);
        Task<Usuario?> RecuperarUsuarioPorIdAsync(int id, CancellationToken ct);
    }
}
