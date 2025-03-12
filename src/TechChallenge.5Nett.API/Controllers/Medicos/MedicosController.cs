using Contatos.Reponses;
using Contatos.Requests;
using Contatos.Servicos;
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
        public async Task<ActionResult<PaginacaoConsulta<MedicoResponse>>> ListarMedicosComPaginacao([FromQuery] MedicosPaginacaoRequest request)
        {
            PaginacaoConsulta<MedicoResponse> paginacaoConsulta = await medicosAppServico.ListarContatosComPaginacaoAsync(request);
            return Ok(paginacaoConsulta);
        }
    }
}