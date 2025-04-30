using Hackathon.Fiap.Domain.Medicos.Entidades;

namespace Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces
{
    public interface IEspecialidadesServico
    {
        Task<Especialidade> ValidarEspecialidadeAsync(int especialidadeId, CancellationToken cancellationToken);

    }
}
