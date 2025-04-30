using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.Medicos.Servicos
{
    public class EspecialidadesServico(IEspecialidadesRepositorio especialidadesRepositorio) : IEspecialidadesServico
    {
        public async Task<Especialidade> ValidarEspecialidadeAsync(int especialidadeId, CancellationToken cancellationToken)
        {
            return await especialidadesRepositorio.RecuperarEspecialidadeAsync(especialidadeId, cancellationToken) ?? throw new RegistroNaoEncontradoExcecao("Especialidade não encontrada!");
        }
    }
}
