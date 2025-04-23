using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;

public class HorarioDisponivelConsulta
{
    public int IdHorarioDisponivel {  get; set; }
    public int IdMedico  { get; set; }
    public int IdPaciente { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public StatusHorarioDisponivelEnum Status { get; set; }
    
}