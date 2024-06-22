using LHS_Domain.RequisicoesConteudo.Repositorios;
using LHS_Application.RequisicoesConteudo.Interfaces;
using LHS_DataTransfer.RequisicoesConteudo.Request;
using LHS_Domain.RequisicoesConteudo.Enumerators;
using LHS_IOT.Bibliotecas;
using LHS_DataTransfer.RequisicoesConteudo.Response;
using LHS_Domain.RequisicoesConteudo.Entidades;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace LHS_Application.RequisicoesConteudo.Servicos
{
    public class RequisicoesConteudoAppServico(IRequisicoesConteudoRepositorio repositorio, IMapper mapper) : IRequisicoesConteudoAppServico
    {
        public void AdicionarNovaRequisicao(RequisicaoConteudoInserirRequest request)
        {
            ValidarRequest(request);
            repositorio.AdicionarRequisicao(request.Titulo, request.Referencia, request.Ano, SituacaoRequisicaoEnum.A);
        }

        private static void ValidarRequest(RequisicaoConteudoInserirRequest request)
        {
            if (request == null)
                throw new Exception("Request inválido");
            if (request.Titulo.Length <= 0)
                throw new Exception("Titulo é obrigatório");
            if (request.Ano > 0 && request.Ano.ToString().Length > 4)
                throw new Exception("Ano inválido");
        }

        public PaginacaoConsulta<RequisicaoConteudoResponse> ListarRequisicoes(RequisicoesConteudoRequest request){
            PaginacaoConsulta<RequisicaoConteudo> response = repositorio.ListarRequisicoes(request);

            return mapper.Map<PaginacaoConsulta<RequisicaoConteudoResponse>>(response);
        }

       public int CancelarRequisicao(int request)
        {
            return repositorio.AlterarSituacaoRequisicao(request, SituacaoRequisicaoEnum.C);
        }

        public int RecusarRequisicao(int id, string observacao)
        {
            repositorio.AlterarSituacaoRequisicao(id, SituacaoRequisicaoEnum.R);
            return repositorio.InserirObservacao(id, observacao);
        }

        public int AtenderRequisicao(int id, string observacao)
        {
            repositorio.AlterarSituacaoRequisicao(id, SituacaoRequisicaoEnum.D);
            return repositorio.InserirObservacao(id, observacao);
        }

        public int FinalizarRequisicao(int id, string observacao)
        {
            int rows = repositorio.AlterarSituacaoRequisicao(id, SituacaoRequisicaoEnum.F);

            if(!observacao.IsNullOrEmpty())
                repositorio.InserirObservacao(id, observacao);

            return rows;
        }
    }
}
