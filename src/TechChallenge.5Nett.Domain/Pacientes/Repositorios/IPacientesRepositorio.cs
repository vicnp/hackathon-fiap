using Pacientes.Entidades;
using Usuarios.Request;
using Utils;

namespace Pacientes.Repositorios
{
    public interface IPacientesRepositorio
    {
        PaginacaoConsulta<Paciente> ListarPacientes(UsuarioListarRequest request);
    }
}