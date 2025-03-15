using Hackathon.Fiap.DataTransfer.Consultas.Enumeradores;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros
{
    public class ConsultasListarFiltro : PaginacaoFiltro
    {
        public int IdMedico { get; set; }
        public int IdPaciente {  get; set; }
        public int IdHorariosDisponiveis {  get; set; }
        public StatusConsultaEnum? Status { get; set; }
        public int IdConsulta {  get; set; }
        public ConsultasListarFiltro() : base("IdConsulta", TipoOrdernacao.Desc)
        {
        }
    }
}
