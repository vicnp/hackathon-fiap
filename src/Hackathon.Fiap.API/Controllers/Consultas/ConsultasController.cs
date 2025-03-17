using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Consultas
{
    [ApiController]
    [Route("api/consultas")]
    public class ConsultasController(IConsultasAppServico consultasAppServico) :ControllerBase
    {
        /// <summary>
        /// Recupera as consultas com paginação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico}")]
        public async Task<ActionResult<PaginacaoConsulta<ConsultaResponse>>> ListarConsultasPaginadasAsync([FromQuery] ConsultaListarRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<ConsultaResponse> response = await consultasAppServico.ListarConsultasAsync(request, ct);
            return Ok(response);
        }

        /// <summary>
        /// Altera situação de uma consulta.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <param name="justificativa"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("situacoes")]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico}")]
        public async Task<ActionResult<ConsultaResponse>> AlterarStatusConsultaAsync([FromQuery] ConsultaStatusRequest request, [FromBody] string justificativa, CancellationToken ct)
        {
            ConsultaResponse response = await consultasAppServico.AlterarStatusConsultaAsync(request, justificativa, ct);
            return Ok(response);
        }
    }
}
