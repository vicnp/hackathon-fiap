namespace Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;

public class HorarioDisponivelInserirRequest
{
    public int IdMedico { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
}