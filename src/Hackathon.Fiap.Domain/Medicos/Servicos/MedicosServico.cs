using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Medicos.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.Medicos.Servicos
{
    public class MedicosServico(IMedicosRepositorio medicosRepositorio) : IMedicosServico
    {
        public async Task<Medico> ValidarMedicoAsync(int medicoId, CancellationToken cancellationToken)
        {
            return await medicosRepositorio.RecuperarMedicoAsync(medicoId, cancellationToken) ?? throw new RegistroNaoEncontradoExcecao("Médico não encontrado!");
        }
    }
}