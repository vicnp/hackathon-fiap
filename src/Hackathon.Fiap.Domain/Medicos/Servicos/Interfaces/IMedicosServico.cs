using Hackathon.Fiap.Domain.Medicos.Entidades;

namespace Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces
{
    public interface IMedicosServico
    {
        Task<Medico> ValidarMedicoAsync(int pacienteId, CancellationToken cancellationToken);
    }
}