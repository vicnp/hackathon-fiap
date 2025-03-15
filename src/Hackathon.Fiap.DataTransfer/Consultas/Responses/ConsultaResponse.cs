using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;

namespace Hackathon.Fiap.DataTransfer.Consultas.Responses
{
    public class ConsultaResponse
    {
        public int IdConsulta { get; set; }
        public MedicoResponse Medico { get; set; } = new MedicoResponse();
        public PacienteResponse Paciente { get; set; } = new PacienteResponse();
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = string.Empty;
        public double Valor { get; set; }
        public string JustificativaCancelamento { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public int IdHorariosDisponiveis { get; set; }
    }
}
