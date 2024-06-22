using LHS_DataTransfer.RequisicoesConteudo.Request;
using LHS_DataTransfer.RequisicoesConteudo.Response;
using LHS_IOT.Bibliotecas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHS_Application.RequisicoesConteudo.Interfaces
{
    public interface IRequisicoesConteudoAppServico
    {
        void AdicionarNovaRequisicao(RequisicaoConteudoInserirRequest request);
        int RecusarRequisicao(int id, string observacao);
        int CancelarRequisicao(int request);
        PaginacaoConsulta<RequisicaoConteudoResponse> ListarRequisicoes(RequisicoesConteudoRequest request);
        int AtenderRequisicao(int id, string observacao);
        int FinalizarRequisicao(int id, string observacao);
    }
}
