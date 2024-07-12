using TC_Domain.Utils;
using TC_Domain.Utils.Enumeradores;

namespace TC_Domain.Contatos.Repositorios.Filtros
{
    public class ContatosPaginadosFiltro : PaginacaoFiltro
    {
        public ContatosPaginadosFiltro() : base("nome", TipoOrdernacao.Desc)
        {
        }

        public int? DDD { get; set; }
        public string? Regiao { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }



    }
}
