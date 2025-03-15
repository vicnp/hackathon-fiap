using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Consultas.Repositorios.Filtros
{
    public class ConsultasListarFiltro : PaginacaoFiltro
    {
        public int IdMedico;
        public int IdPaciente;
        public int IdHorariosDisponiveis;
        public ConsultasListarFiltro() : base("IdConsulta", TipoOrdernacao.Desc)
        {
        }
    }
}
