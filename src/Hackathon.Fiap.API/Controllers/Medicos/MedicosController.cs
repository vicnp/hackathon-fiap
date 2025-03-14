using Hackathon.Fiap.Application.Medicos.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Medicos
{
    [ApiController]
    [Route("api/medicos")]
    public class MedicosController(IMedicosAppServico medicosAppServico) : ControllerBase
    {
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico}")]
        public async Task<ActionResult<PaginacaoConsulta<MedicoResponse>>> ListarMedicosComPaginacaoAsync([FromQuery] MedicosPaginacaoRequest request)
        {
            PaginacaoConsulta<MedicoResponse> paginacaoConsulta = await medicosAppServico.ListarMedicosComPaginacaoAsync(request);
            return Ok(paginacaoConsulta);
        }
    }
}