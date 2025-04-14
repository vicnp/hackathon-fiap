using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;
using Hackathon.Fiap.DataTransfer.Utils;

namespace Hackathon.Fiap.Application.HorariosDisponiveis.Interfaces;

public interface IHorariosDisponiveisAppServico
{
    Task<PaginacaoConsulta<HorarioDisponivelResponse>> ListarHorariosDisponiveisAsync(HorarioDisponivelListarRequest request, CancellationToken ct);
    Task InserirHorariosDisponiveisAsync(HorarioDisponivelInserirRequest request, CancellationToken ct);

}