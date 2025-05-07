using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios
{
    public interface IEspecialidadesRepositorio
    {
        Task<PaginacaoConsulta<Especialidade>> ListarEspecialidadesMedicosPaginadosAsync(EspecialidadePaginacaoFiltro filtro, CancellationToken ct);
        Task<Especialidade?> RecuperarEspecialidadeAsync(int especialidadeId, CancellationToken ct);
        Task<Especialidade> InserirEspecialidadeAsync(Especialidade novoUsuario, CancellationToken ct);
        Task DeletarEspecialidadeAsync(int especialidadeId, CancellationToken ct);
        Task<IEnumerable<Especialidade>> ListarEspecialidadesMedicoAsync(int medicoId, CancellationToken ct);
    }
}
