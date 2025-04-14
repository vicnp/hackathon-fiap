using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaListarRequest : PaginacaoFiltro
    {
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public int IdHorariosDisponiveis { get; set; }
        public StatusConsultaEnum Status { get; set; }
        public ConsultaListarRequest() : base("IdConsulta", TipoOrdernacao.Desc)
        {
        }
    }
}
