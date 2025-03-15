using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Consultas.Consultas;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios
{
    public interface IConsultasRepositorio
    {
        Task<PaginacaoConsulta<ConsultaConsulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
