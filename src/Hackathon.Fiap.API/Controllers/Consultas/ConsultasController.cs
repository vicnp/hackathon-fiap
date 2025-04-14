using Hackathon.Fiap.Application.Consultas.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Consultas.Entidades;
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
        /// Insere consultas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = $"{Roles.Medico},{Roles.Administrador},{Roles.Paciente}")]
        public async Task<ActionResult> InserirHorariosDisponiveisAsync([FromBody] ConsultaRequest request, CancellationToken ct)
        {
            ConsultaResponse response = await consultasAppServico.InserirConsultaAsync(request, ct);
            return Created("api/consultas/inserir", response);
        }

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
        public async Task<ActionResult<ConsultaResponse>> AlterarStatusConsultaAsync([FromBody] ConsultaStatusRequest request, CancellationToken ct)
        {
            ConsultaResponse response = await consultasAppServico.AlterarStatusConsultaAsync(request, request.Justificativa, ct);
            return Ok(response);
        }
    }
}
