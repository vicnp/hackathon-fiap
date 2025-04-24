namespace Hackathon.Fiap.Domain.Consultas.Consultas
{
    public class ConsultaConsulta
    {
        public int ConsultaId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public double Valor { get; set; }
        public string? Status { get; set; }
        public string JustificativaCancelamento { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public int HorarioDisponivelId { get; set; }
    }
}
