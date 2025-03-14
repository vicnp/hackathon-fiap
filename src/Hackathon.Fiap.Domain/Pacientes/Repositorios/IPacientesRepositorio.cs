using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Domain.Pacientes.Repositorios
{
    public interface IPacientesRepositorio
    {
        PaginacaoConsulta<Paciente> ListarPacientes(UsuarioListarRequest request);
    }
}