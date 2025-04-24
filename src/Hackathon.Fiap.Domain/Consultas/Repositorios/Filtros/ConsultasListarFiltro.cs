using Hackathon.Fiap.Domain.Consultas.Enumeradores;
using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros
{
    public class ConsultasListarFiltro : PaginacaoFiltro
    {
        public int MedicoId { get; set; }
        public int PacienteId {  get; set; }
        public int HorarioDisponivelId {  get; set; }
        public StatusConsultaEnum? Status { get; set; }
        public int ConsultaId {  get; set; }
        public ConsultasListarFiltro() : base("ConsultaId", TipoOrdernacao.Desc)
        {
        }
    }
}
