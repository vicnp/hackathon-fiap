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
    [Route("api/especialidades")]
    public class EspecialidadesController(IEspecialidadeAppServico especialidadeAppServico) : ControllerBase
    {
        /// <summary>
        /// Insere nova especialidade de medico.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> InserirEspecialidadeAsync([FromBody] EspecialidadeRequest request, CancellationToken ct)
        {
            EspecialidadeResponse response = await especialidadeAppServico.InserirEspecialidadeAsync(request, ct);
            return Created("api/consultas/especialidades", response);
        }

        /// <summary>
        /// Remove uma especialidade no sistema.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = Roles.Administrador)]
        public async Task<ActionResult> DeletarEspecialidadeAsync(int id, CancellationToken ct)
        {
            await especialidadeAppServico.DeletarEspecialidadeAsync(id, ct);
            return Ok();
        }

        /// <summary>
        /// Recuperar especialidade por ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = $"{Roles.Medico},{Roles.Administrador}, {Roles.Paciente}")]
        public async Task<ActionResult<EspecialidadeResponse>> RecuperarEspecialidadeAsync(int id, CancellationToken ct)
        {
            EspecialidadeResponse? response = await especialidadeAppServico.RecuperarEspecialidadeAsync(id, ct);
            return Ok(response);
        }

        /// <summary>
        /// Consulta de especialidade.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Lista paginada de usuários</returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = $"{Roles.Medico},{Roles.Administrador}")]
        public async Task<ActionResult<PaginacaoConsulta<EspecialidadeResponse>>> ListarEspecialidadesMedicosPaginadosAsync([FromQuery] EspecialidadesPaginacaoRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<EspecialidadeResponse> paginacaoConsulta = await especialidadeAppServico.ListarEspecialidadesMedicosPaginadosAsync(request, ct);
            return Ok(paginacaoConsulta);
        }
    }
}
