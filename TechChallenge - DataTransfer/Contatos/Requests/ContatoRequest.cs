using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_IOC.Bibliotecas;

namespace TC_DataTransfer.Contatos.Requests
{
    public class ContatoRequest : PaginacaoFiltro
    {
        public ContatoRequest() : base("nome", TipoOrdernacao.Desc)
        {
        }

        public int? DDD { get; set; }
        public string? Regiao { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }

    }
}
