using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios
{
    public interface IMedicosRepositorio
    {
        Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro, CancellationToken ct);
        Task<Medico?> RecuperarMedico(int codigoMedico, CancellationToken ct);
    }
}