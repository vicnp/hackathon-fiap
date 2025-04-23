using Hackathon.Fiap.Domain.Consultas.Consultas;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios
{
    public interface IConsultasRepositorio
    {
        Task<int> AtualizarStatusConsultaAsync(Consulta consulta, CancellationToken ct);
        Task<PaginacaoConsulta<ConsultaConsulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
