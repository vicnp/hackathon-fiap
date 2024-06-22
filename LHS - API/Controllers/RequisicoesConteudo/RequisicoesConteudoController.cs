using TC_Application.Usuarios.Interfaces;
using TC_DataTransfer.Usuarios.Request;
using TC_DataTransfer.Usuarios.Response;
using TC_IOT.Bibliotecas;
using Microsoft.AspNetCore.Mvc;
using TC_DataTransfer.RequisicoesConteudo.Request;
using TC_Application.RequisicoesConteudo.Interfaces;
using TC_DataTransfer.RequisicoesConteudo.Response;

namespace LHS___API.Controllers.RequisicoesConteudo
{
    [Route("api/requisicoes")]
    [ApiController]
    public class UsuariosController(IRequisicoesConteudoAppServico requisicoes) : ControllerBase
    {
        /// <summary>
        /// Insere uma nova requisição de adicão de conteudo no servidor.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CriarRequisicao([FromBody] RequisicaoConteudoInserirRequest request)
        {
            requisicoes.AdicionarNovaRequisicao(request);
            return Ok();
        }

        /// <summary>
        /// Lista as requisições criadas.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<PaginacaoConsulta<RequisicaoConteudoResponse>> ListarRequisicoes([FromQuery] RequisicoesConteudoRequest request)
        {
            PaginacaoConsulta<RequisicaoConteudoResponse> paginacaoConsulta = requisicoes.ListarRequisicoes(request);
            return Ok(paginacaoConsulta);
        }

        /// <summary>
        /// Cancela uma requisição.
        /// </summary>
        /// <param name="request">Código da requisição</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public ActionResult<int> CancelarRequisicao([FromRoute] int id)
        {
            int rows = requisicoes.CancelarRequisicao(id);
            return Ok(rows);
        }

        /// <summary>
        /// Recusar uma requisição.
        /// </summary>
        /// <param name="id">Código da requisição, observacao e situacao</param>
        /// <param name="observacao">Código da requisição, observacao e situacao</param>
        /// <returns></returns>
        [HttpPut]
        [Route("recusas/{id}/{observacao}")]
        public ActionResult<int> RecusarRequisicao([FromRoute] int id, string observacao)
        {
            int rows = requisicoes.RecusarRequisicao(id, observacao);
            return Ok(rows);
        }

        /// <summary>
        /// Atender uma requisição.
        /// </summary>
        /// <param name="id">Código da requisição, observacao e situacao</param>
        /// <param name="observacao">Código da requisição, observacao e situacao</param>
        /// <returns></returns>
        [HttpPut]
        [Route("atendimentos/{id}/{observacao}")]
        public ActionResult<int> AtenderRequisicao([FromRoute] int id, string observacao)
        {
            int rows = requisicoes.AtenderRequisicao(id, observacao);
            return Ok(rows);
        }

        /// <summary>
        /// Atender uma requisição.
        /// </summary>
        /// <param name="id">Código da requisição, observacao e situacao</param>
        /// <param name="observacao">Código da requisição, observacao e situacao</param>
        /// <returns></returns>
        [HttpPut]
        [Route("finalizacoes/{id}/{observacao}")]
        public ActionResult<int> FinalizacoesRequisicao([FromRoute] int id, string observacao)
        {
            int rows = requisicoes.FinalizarRequisicao(id, observacao);
            return Ok(rows);
        }
    }
}
