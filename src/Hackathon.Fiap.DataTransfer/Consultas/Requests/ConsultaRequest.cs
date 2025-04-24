using Hackathon.Fiap.Domain.Consultas.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaRequest
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int HorarioDisponivelId { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public double Valor { get; set; }
    }
}
