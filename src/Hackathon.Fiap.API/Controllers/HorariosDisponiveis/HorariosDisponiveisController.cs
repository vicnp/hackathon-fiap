using Hackathon.Fiap.Application.HorariosDisponiveis.Interfaces;
using Hackathon.Fiap.DataTransfer.Consultas.Requests;
using Hackathon.Fiap.DataTransfer.Consultas.Responses;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Requests;
using Hackathon.Fiap.DataTransfer.HorariosDisponiveis.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.HorariosDisponiveis
{
    [ApiController]
    [Route("api/horarios-disponiveis")]
    public class HorariosDisponiveisController(IHorariosDisponiveisAppServico horariosDisponiveisAppServico)
        : ControllerBase
    {
        
        /// <summary>
        /// Recupera os horários disponíveis para consultas com paginação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico},{Roles.Administrador}")]
        public async Task<ActionResult<PaginacaoConsulta<HorarioDisponivelResponse>>> ListarHorariosDisponiveisPaginadosAsync([FromQuery] HorarioDisponivelListarRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<HorarioDisponivelResponse> response = await horariosDisponiveisAppServico.ListarHorariosDisponiveisAsync(request, ct);
            return Ok(response);
        }
        
        
        /// <summary>
        /// Insere horários disponíveis para consultas
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("inserir")]
        [Authorize(Roles = $"{Roles.Medico},{Roles.Administrador}")]
        public async Task<ActionResult<HorarioDisponivelResponse>> InserirHorariosDisponiveisAsync([FromBody] HorarioDisponivelInserirRequest request, CancellationToken ct)
        {
            await horariosDisponiveisAppServico.InserirHorariosDisponiveisAsync(request, ct);
            return Created();
        }
    }
}