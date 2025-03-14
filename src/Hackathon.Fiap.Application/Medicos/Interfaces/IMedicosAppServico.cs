using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Utils;

namespace Hackathon.Fiap.Application.Medicos.Interfaces
{
    public interface IMedicosAppServico
    {
        Task<PaginacaoConsulta<MedicoResponse>> ListarMedicosComPaginacaoAsync(MedicosPaginacaoRequest request);
    }
}