using Hackathon.Fiap.Application.Medicos.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Medicos
{
    [ApiController]
    [Route("api/medicos")]
    public class MedicosController(IMedicosAppServico medicosAppServico) : ControllerBase
    {
        /// <summary>
        /// Recuperar Médicos com paginação
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico}")]
        public async Task<ActionResult<PaginacaoConsulta<MedicoResponse>>> ListarMedicosComPaginacaoAsync([FromQuery] MedicosPaginacaoRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<MedicoResponse> paginacaoConsulta = await medicosAppServico.ListarMedicosComPaginacaoAsync(request, ct);
            return Ok(paginacaoConsulta);
        }
    }
}