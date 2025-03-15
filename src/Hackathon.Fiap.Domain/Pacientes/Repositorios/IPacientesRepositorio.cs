using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios.Filtros;

namespace Hackathon.Fiap.Domain.Pacientes.Repositorios
{
    public interface IPacientesRepositorio
    {
        Task<PaginacaoConsulta<Paciente>> ListarPacientesAsync(UsuarioListarFiltro request, CancellationToken ct);
        Task<Paciente?> RecuperarPaciente(int idPaciente, CancellationToken ct);
    }
}