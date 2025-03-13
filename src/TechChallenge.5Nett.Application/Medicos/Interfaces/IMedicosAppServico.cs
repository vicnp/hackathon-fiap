using Medicos.Requests;
using Medicos.Responses;
using Utils;

namespace Medicos.Interfaces
{
    public interface IMedicosAppServico
    {
        Task<PaginacaoConsulta<MedicoResponse>> ListarMedicosComPaginacaoAsync(MedicosPaginacaoRequest request);
    }
}