using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;

public class HorarioDisponivelResponse
{
    public int IdConsulta { get; set; }
    public MedicoResponse Medico { get; set; } = new MedicoResponse();
    public PacienteResponse Paciente { get; set; } = new PacienteResponse();
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public StatusHorarioDisponivelEnum Status { get; set; }
}