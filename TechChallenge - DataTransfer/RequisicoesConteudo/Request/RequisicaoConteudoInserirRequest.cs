using LHS_Domain.RequisicoesConteudo.Enumerators;

namespace LHS_DataTransfer.RequisicoesConteudo.Request
{
    public class RequisicaoConteudoInserirRequest
    {
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public string Referencia { get; set;}
        public SituacaoRequisicaoEnum Situacao { get; set; }
    }
}
