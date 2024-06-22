using Dapper;
using TC_IOT.Bibliotecas;
using TC_IOT.DBContext;
using TC_Domain.RequisicoesConteudo.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_DataTransfer.Usuarios.Request;
using TC_Domain.Usuarios.Entidades;
using TC_Domain.RequisicoesConteudo.Entidades;
using TC_DataTransfer.RequisicoesConteudo.Request;
using Mysqlx.Expr;

namespace TC_Domain.RequisicoesConteudo.Repositorios
{
    public class RequisicoesConteudoRepositorio(DapperContext dapperContext) : RepositorioDapper<RequisicaoConteudo>(dapperContext), IRequisicoesConteudoRepositorio
    {
        public void AdicionarRequisicao(string titulo, string referencia, int ano, SituacaoRequisicaoEnum a)
        {
            string SQL = $@"
                            INSERT INTO `delta`.`lhs_requisicoes`
                            (`titulo`,
                            `ano`,
                            `referencia`,
                            `situacao`
                            )
                            VALUES (
                                @TITULO,
                                @ANO,
                                @REFERENCIA,
                                'A'
                            );
                        ";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("TITULO", titulo);
            dynamicParameters.Add("ANO", ano);
            dynamicParameters.Add("REFERENCIA", referencia);

            using var con = dapperContext.CreateConnection();
            con.Execute(SQL, dynamicParameters);

        }
     
        public PaginacaoConsulta<RequisicaoConteudo> ListarRequisicoes(RequisicoesConteudoRequest request)
        {


            string SQL = $@"
                    select 
	                    req.titulo as Titulo,
                        req.ano as Ano,
                        req.observacao as Observacao,
                        req.referencia as Referencia,
                        req.id as Id,
                        req.situacao as Situacao
                    from lhs_requisicoes req
                    where 1 = 1 ";

            if (request.Titulo != null && request.Titulo.Length > 0)
                SQL += $@"
                            and req.titulo like '%{request.Titulo}%' ";

            if (request.Situacao != 0 && request.Situacao != null)
                SQL += $@"
                            and re.situacao = {request.Situacao} ";

            return ListarPaginado(SQL.ToString(), request.Pg, request.Qt, request.CpOrd, request.TpOrd.ToString());
        }

        public int AlterarSituacaoRequisicao(int request, SituacaoRequisicaoEnum novaSituacao)
        {
            string SQL = $@"
                            UPDATE delta.lhs_requisicoes r
                             SET  r.situacao = @SITUACAO
                            WHERE r.id = @CODIGO
                        ";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("CODIGO", request);
            dynamicParameters.Add("SITUACAO", novaSituacao.ToString());

            using var con = dapperContext.CreateConnection();
            return con.Execute(SQL, dynamicParameters);
        }

        public int InserirObservacao(int id, string observacao)
        {
            string SQL = $@"
                            UPDATE delta.lhs_requisicoes r
                             SET  r.observacao = @OBSERVACAO
                            WHERE r.id = @CODIGO
                        ";
            DynamicParameters dynamicParameters = new();
            dynamicParameters.Add("CODIGO", id);
            dynamicParameters.Add("OBSERVACAO", observacao);

            using var con = dapperContext.CreateConnection();
            return con.Execute(SQL, dynamicParameters);

        }
    }
}
