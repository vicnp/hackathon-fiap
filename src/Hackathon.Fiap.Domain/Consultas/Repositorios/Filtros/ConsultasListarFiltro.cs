using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

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
