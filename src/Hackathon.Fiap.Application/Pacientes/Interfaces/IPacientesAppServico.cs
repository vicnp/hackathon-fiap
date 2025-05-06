using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.Domain.Utils;

namespace Hackathon.Fiap.Application.Pacientes.Interfaces
{
    public interface IPacientesAppServico
    {
        Task<PaginacaoConsulta<PacienteResponse>> ListarPacientesPaginadosAsync(UsuarioListarRequest request, CancellationToken ct);
    }
}