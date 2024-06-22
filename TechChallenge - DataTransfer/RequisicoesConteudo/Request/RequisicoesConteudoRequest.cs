using TC_Domain.RequisicoesConteudo.Enumerators;
using TC_IOC.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_DataTransfer.RequisicoesConteudo.Request
{
    public class RequisicoesConteudoRequest : PaginacaoFiltro
    {
        public RequisicoesConteudoRequest() : base("Id", TipoOrdernacao.Desc)
        {
        }

        public string? Titulo {  get; set; }
        public SituacaoRequisicaoEnum? Situacao { get; set; }
      
    }
}
