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
        public ActionResult<ContatoResponse> InserirContato(ContatoInserirRequest request) 
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
    }

}
