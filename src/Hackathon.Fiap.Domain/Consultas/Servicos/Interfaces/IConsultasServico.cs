using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Domain.Consultas.Servicos.Interfaces
{
    public interface IConsultasServico
    {
        Task<Consulta?> AtualizarStatusConsultaAsync(Consulta consulta, StatusConsultaEnum status, CancellationToken ct);
        Task<Consulta> InserirConsultaAsync(ConsultaInserirFiltro filtro, CancellationToken ct);
        Task<PaginacaoConsulta<Consulta>> ListarConsultasAsync(ConsultasListarFiltro filtro, CancellationToken ct);
        Task<Consulta?> RecuperarConsultaAsync(ConsultasListarFiltro filtro, CancellationToken ct);
    }
}
