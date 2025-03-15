using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Consultas
{
    [ApiController]
    [Route("api/consultas")]
    public class ConsultasController(IConsultasAppServico consultasAppServico) :ControllerBase
    {
        /// <summary>
        /// Recupera as consultas cadastradas conforme os filtros informados.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        public async Task<ActionResult<PaginacaoConsulta<ConsultaResponse>>> ListarConsultasPaginadasAsync([FromQuery] ConsultaListarRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<ConsultaResponse> response = await consultasAppServico.ListarConsultasAsync(request, ct);
            return Ok(response);
        }
    }
}
