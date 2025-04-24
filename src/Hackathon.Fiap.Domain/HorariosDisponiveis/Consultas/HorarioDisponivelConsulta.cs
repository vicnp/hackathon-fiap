using Hackathon.Fiap.Domain.HorariosDisponiveis.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Consultas;

public class HorarioDisponivelConsulta
{
    public int HorarioDisponivelId {  get; set; }
    public int MedicoId  { get; set; }
    public int PacienteId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public StatusHorarioDisponivelEnum Status { get; set; }
    
}