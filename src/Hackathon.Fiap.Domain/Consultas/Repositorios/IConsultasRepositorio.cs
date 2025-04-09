using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Infra.Consultas.Consultas;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios
{
    public interface IConsultasRepositorio
    {
        Task<int> AtualizarStatusConsultaAsync(Consulta consulta, CancellationToken ct);
        Task<Consulta> InserirConsultaAsync(Consulta consulta, CancellationToken ct);
        Task<PaginacaoConsulta<ConsultaConsulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
