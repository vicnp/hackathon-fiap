using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;

namespace Hackathon.Fiap.Domain.Pacientes.Servicos
{
    public class PacienteServico(IPacientesRepositorio pacientesRepositorio) : IPacientesServicos
    {
        public async Task<Paciente> ValidarPacienteAsync(int pacienteId, CancellationToken cancellationToken)
        {
            return await pacientesRepositorio.RecuperarPaciente(pacienteId, cancellationToken) ?? throw new RegistroNaoEncontradoExcecao("Paciente não encontrado!");
        }
    }
}
