using Medicos.Interfaces;
using Medicos.Requests;
using Medicos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Entidades;
using Utils;

namespace Controllers.Medicos
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