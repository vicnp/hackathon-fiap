using Microsoft.AspNetCore.Mvc;
using TC_Application.Contatos.Interfaces;
using TC_DataTransfer.Contatos.Requests;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using TC_Domain.Utils;
using Microsoft.AspNetCore.Authorization;

namespace TC_API.Controllers.Contatos
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatosController(IContatosAppServico contatosAppServico): ControllerBase
    {
        /// <summary>
        /// Lista os contatos, permitindo filtragem.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Listagem paginada de contatos.</returns>
        [HttpGet]
        //[Authorize]
        public ActionResult<PaginacaoConsulta<ContatoResponse>> ListarContatosComPaginacao([FromQuery]ContatoRequest request)
        {
            return Ok(contatosAppServico.ListarContatosComPaginacao(request));
        }

        /// <summary>
        /// Realiza o cadastro de um contato na base de dados.
        /// </summary>
        /// <param name="request">Dados para cadastro do contato.</param>
        /// <returns>O contato cadastrado.</returns>
        [HttpPost]
        [Authorize]
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
        [HttpDelete]
        [Authorize]
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
        [HttpPut]
        [Authorize]
        public ActionResult<ContatoResponse> AtualizarContato(int id, ContatoCrudRequest request)
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
