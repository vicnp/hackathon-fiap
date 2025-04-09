using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios
{
    public interface IMedicosRepositorio
    {
        Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro, CancellationToken ct);
        Task<Medico?> RecuperarMedicoAsync(int medicoId, CancellationToken ct);
    }
}