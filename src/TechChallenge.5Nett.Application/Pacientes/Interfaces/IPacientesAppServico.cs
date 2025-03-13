using Pacientes.Responses;
using Usuarios.Request;
using Utils;

namespace Pacientes.Interfaces
{
    public interface IPacientesAppServico
    {
        PaginacaoConsulta<PacienteResponse> ListarPacientes(UsuarioListarRequest request);
    }
}