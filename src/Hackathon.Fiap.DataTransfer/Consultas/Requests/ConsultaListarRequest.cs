using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.DataTransfer.Consultas.Requests
{
    public class ConsultaListarRequest : PaginacaoFiltro
    {
        public int IdMedico;
        public int IdPaciente;
        public int IdHorariosDisponiveis;
        public ConsultaListarRequest() : base("IdConsulta", TipoOrdernacao.Desc)
        {
        }
    }
}
