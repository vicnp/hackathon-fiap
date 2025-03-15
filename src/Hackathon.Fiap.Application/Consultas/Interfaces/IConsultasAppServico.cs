using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Utils;

namespace Hackathon.Fiap.Application.Consultas.Interfaces
{
    public interface IConsultasAppServico
    {
        Task<PaginacaoConsulta<ConsultaResponse>> ListarConsultasAsync(ConsultaListarRequest request, CancellationToken ct);
    }
}
