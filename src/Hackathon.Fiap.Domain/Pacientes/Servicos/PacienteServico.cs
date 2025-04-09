using Hackathon.Fiap.Domain.Medicos.Entidades;
using Hackathon.Fiap.Domain.Medicos.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Entidades;
using Hackathon.Fiap.Domain.Pacientes.Repositorios;
using Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
