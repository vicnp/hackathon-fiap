using Microsoft.AspNetCore.Mvc;
using TC_Application.Contatos.Interfaces;
using TC_DataTransfer.Contatos.Requests;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using YCTC_DataTransfer.Contatos.Requests;

namespace TC_API.Controllers.Contatos
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatosController(IContatosAppServico contatosAppServico) : ControllerBase
    {
        /// <summary>
        /// Lista os contatos, permitindo filtragem.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Listagem paginada de contatos.</returns>
        [HttpGet]
        //[Authorize]
        public ActionResult<PaginacaoConsulta<ContatoResponse>> ListarContatosComPaginacao([FromQuery] ContatoPaginacaoRequest request)
        {
            return Ok(contatosAppServico.ListarContatosComPaginacao(request));
        }

        /// <summary>
        /// Lista os contatos, permitindo filtragem.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Listagem paginada de contatos.</returns>
        [HttpGet("itens")]
        //[Authorize]
        public ActionResult<List<ContatoResponse>> ListarContatosSemPaginacao([FromQuery] ContatoRequest request)
        {
            return Ok(contatosAppServico.ListarContatosSemPaginacao(request));
        }

        /// <summary>
        /// Lista os contatos, permitindo filtragem.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Listagem paginada de contatos.</returns>
        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<List<ContatoResponse>> RecuperarContato(int id)
        {
            return Ok(contatosAppServico.RecuperarContato(id));
        }

        /// <summary>
        /// Realiza o cadastro de um contato na base de dados.
        /// </summary>
        /// <param name="request">Dados para cadastro do contato.</param>
        /// <returns>O contato cadastrado.</returns>
        [HttpPost]
        //[Authorize]
        public ActionResult<ContatoResponse> InserirContato(ContatoCrudRequest request)
        {
            try
            {
                return Ok(contatosAppServico.InserirContato(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Remove um contato da base de dados.
        /// </summary>
        /// <param name="id">Código do contato a ser removido</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        //[Authorize]
        public ActionResult<ContatoResponse> RemoverContato(int id)
        {
            try
            {
                contatosAppServico.RemoverContato(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Atualiza os dados de um contato.
        /// </summary>
        /// <param name="id">Código do contato a ser editado</param>
        /// <param name="request">Dados atualizados</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        //[Authorize]
        public ActionResult<ContatoResponse> AtualizarContato(int id, [FromBody] ContatoCrudRequest request)
        {
            try
            {
                ContatoResponse? contatoResponse = contatosAppServico.AtualizarContato(request, id);
                if(contatoResponse == null)
                    return BadRequest("Contato não encontrado.");

                return Ok(contatoResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

}
