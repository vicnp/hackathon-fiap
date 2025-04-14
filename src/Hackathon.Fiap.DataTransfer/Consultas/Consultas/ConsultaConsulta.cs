namespace Hackathon.Fiap.Infra.Consultas.Consultas
{
    public class ConsultaConsulta
    {
        public int IdConsulta { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public DateTime DataHora { get; set; }
        public double Valor { get; set; }
        public string Status { get; set; } = string.Empty;
        public string JustificativaCancelamento { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; }
        public int IdHorariosDisponiveis { get; set; }
    }
}
