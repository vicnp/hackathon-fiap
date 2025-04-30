using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Medicos.Interfaces
{
    public interface IEspecialidadeAppServico
    {
        Task<PaginacaoConsulta<EspecialidadeResponse>> ListarEspecialidadesMedicosPaginadosAsync(EspecialidadesPaginacaoRequest request, CancellationToken ct);
        Task<EspecialidadeResponse> RecuperarEspecialidadeAsync(int especialidadeId, CancellationToken ct);
        Task<EspecialidadeResponse> InserirEspecialidadeAsync(EspecialidadeRequest especialidade, CancellationToken ct);
        Task DeletarEspecialidadeAsync(int especialidadeId, CancellationToken ct);
    }
}
