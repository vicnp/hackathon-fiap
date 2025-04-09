using Hackathon.Fiap.Domain.Pacientes.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Fiap.Domain.Pacientes.Servicos.Interfaces
{
    public interface IPacientesServicos
    {
        Task<Paciente> ValidarPacienteAsync(int pacienteId, CancellationToken cancellationToken);
    }
}
