using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;

namespace Hackathon.Fiap.Application.Pacientes.Interfaces
{
    public interface IPacientesAppServico
    {
        PaginacaoConsulta<PacienteResponse> ListarPacientes(UsuarioListarRequest request);
    }
}