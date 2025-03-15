using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;

namespace Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces
{
    public interface IConsultaServico
    {
        Task<PaginacaoConsulta<Consulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
