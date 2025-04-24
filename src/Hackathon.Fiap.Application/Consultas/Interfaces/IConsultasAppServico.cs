using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Consultas.Interfaces
{
    public interface IConsultasAppServico
    {
        Task<ConsultaResponse> AlterarStatusConsultaAsync(ConsultaStatusRequest request, string? justificativa, CancellationToken ct);
        Task<ConsultaResponse> InserirConsultaAsync(ConsultaRequest request, CancellationToken ct);
        Task<PaginacaoConsulta<ConsultaResponse>> ListarConsultasAsync(ConsultaListarRequest request, CancellationToken ct);
    }
}
