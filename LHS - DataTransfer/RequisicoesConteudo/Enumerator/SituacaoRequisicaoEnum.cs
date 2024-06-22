using System.ComponentModel;

namespace LHS_Domain.RequisicoesConteudo.Enumerators
{
    public enum SituacaoRequisicaoEnum
    {
        [Description("Aguardando")]
        A = 'A',
        [Description("Downloading")]
        D = 'D',
        [Description("Finalizado")]
        F = 'F',
        [Description("Recusado")]
        R = 'R',
        [Description("Cancelado")]
        C = 'C'
    }
}
