using LHS_DataTransfer.RequisicoesConteudo.Request;
using LHS_DataTransfer.RequisicoesConteudo.Response;
using LHS_Domain.RequisicoesConteudo.Entidades;
using LHS_Domain.RequisicoesConteudo.Enumerators;
using LHS_IOT.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHS_Domain.RequisicoesConteudo.Repositorios
{
    public interface IRequisicoesConteudoRepositorio
    {
        void AdicionarRequisicao(string titulo, string referencia, int ano, SituacaoRequisicaoEnum a);
        int AlterarSituacaoRequisicao(int request, SituacaoRequisicaoEnum c);
        int InserirObservacao(int id, string observacao);
        PaginacaoConsulta<RequisicaoConteudo> ListarRequisicoes(RequisicoesConteudoRequest request);
    }
}
