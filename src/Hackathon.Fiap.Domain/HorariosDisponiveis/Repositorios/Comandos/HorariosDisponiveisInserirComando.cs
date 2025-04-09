namespace Hackathon.Fiap.Domain.HorariosDisponiveis.Repositorios.Comandos;

public class HorariosDisponiveisInserirComando
{
    public int MedicoId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
}