using Hackathon.Fiap.Domain.Utils;
using Hackathon.Fiap.Domain.Utils.Enumeradores;

namespace Hackathon.Fiap.Domain.Medicos.Repositorios.Filtros
{
    public class EspecialidadePaginacaoFiltro : PaginacaoFiltro
    {
        public EspecialidadePaginacaoFiltro() : base("NomeEspecialidade", TipoOrdernacao.Desc)
        {
        }

        public int? CodigoEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; } = string.Empty;
    }
}