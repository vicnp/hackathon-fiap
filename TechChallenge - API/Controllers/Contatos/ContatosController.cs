using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC_Application.Contatos.Interfaces;
using TC_Domain.Contatos.Servicos.Interfaces;
using TC_DataTransfer.Contatos.Requests;
using TC_IOC.Bibliotecas;
using TC_DataTransfer.Contatos.Reponses;
using YCTC_DataTransfer.Contatos.Requests;

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
        /// <returns>Listagem pagina de contatos.</returns>
        [HttpGet]
        //[Authorize]
        public ActionResult<PaginacaoConsulta<ContatoResponse>> ListarContatosComPaginacao([FromQuery]ContatoRequest request)
        {
            return Ok(contatosAppServico.ListarContatosComPaginacao(request));
        }

        [HttpPost]
        //[Authorize]
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
