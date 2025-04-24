using Hackathon.Fiap.Domain.Pacientes.Entidades;

namespace Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces
{
    public interface IPacientesServicos
    {
        Task<Paciente> ValidarPacienteAsync(int pacienteId, CancellationToken cancellationToken);
    }
}
