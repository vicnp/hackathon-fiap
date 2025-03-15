using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios
{
    public interface IMedicosRepositorio
    {
        Task<PaginacaoConsulta<Medico>> ListarMedicosPaginadosAsync(MedicosPaginacaoFiltro filtro);
        Task<Medico?> RecuperarMedico(int codigoMedico);
    }
}