using Medicos.Entidades;
using Medicos.Repositorios.Filtros;
using Utils;

namespace Medicos.Repositorios
{
    public interface IMedicosRepositorio
    {
        Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro);
    }
}