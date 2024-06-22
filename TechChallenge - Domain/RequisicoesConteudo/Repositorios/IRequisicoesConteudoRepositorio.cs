using TC_DataTransfer.RequisicoesConteudo.Request;
using TC_DataTransfer.RequisicoesConteudo.Response;
using TC_Domain.RequisicoesConteudo.Entidades;
using TC_Domain.RequisicoesConteudo.Enumerators;
using TC_IOC.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.RequisicoesConteudo.Repositorios
{
    public interface IRequisicoesConteudoRepositorio
    {
        void AdicionarRequisicao(string titulo, string referencia, int ano, SituacaoRequisicaoEnum a);
        int AlterarSituacaoRequisicao(int request, SituacaoRequisicaoEnum c);
        int InserirObservacao(int id, string observacao);
        PaginacaoConsulta<RequisicaoConteudo> ListarRequisicoes(RequisicoesConteudoRequest request);
    }
}
