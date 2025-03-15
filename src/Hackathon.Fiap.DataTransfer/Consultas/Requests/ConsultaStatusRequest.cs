using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaStatusRequest
    {
        public int IdConsulta {  get; set; }
        public StatusConsultaEnum Status {  get; set; } 
    }
}
