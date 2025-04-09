using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaListarRequest : PaginacaoFiltro
    {
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public int HorarioDisponiveId { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public ConsultaListarRequest() : base("ConsultaId", TipoOrdernacao.Desc)
        {
        }
    }
}
