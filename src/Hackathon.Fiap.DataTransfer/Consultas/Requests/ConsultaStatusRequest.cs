using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaStatusRequest
    {
        public int ConsultaId {  get; set; }
        public StatusConsultaEnum Status {  get; set; } 
        public string? Justificativa { get; set; }
    }
}
