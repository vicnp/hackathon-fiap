using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;

namespace Hackathon.Fiap.Domain.Pacientes.Repositorios
{
    public interface IPacientesRepositorio
    {
        PaginacaoConsulta<Paciente> ListarPacientes(UsuarioListarFiltro request);
        Task<Paciente?> RecuperarPaciente(int idPaciente);
    }
}