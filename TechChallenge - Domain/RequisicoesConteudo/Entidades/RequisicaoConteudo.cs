using TC_Domain.RequisicoesConteudo.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.RequisicoesConteudo.Entidades
{
    public class RequisicaoConteudo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Observacao { get; set; }
        public string Referencia { get; set; }
        public SituacaoRequisicaoEnum Situacao { get; set; }
    }
}
