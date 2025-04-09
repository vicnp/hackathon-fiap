namespace Hackathon.Fiap.Infra.Consultas.Consultas
{
    public class ConsultaConsulta
    {
        public int ConsultaId { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; } = string.Empty;
        public string JustificativaCancelamento { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public int HorarioDisponivelId { get; set; }
    }
}
