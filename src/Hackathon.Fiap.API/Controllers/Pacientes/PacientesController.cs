using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.DataTransfer.Pacientes.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.Application.Pacientes.Interfaces;
using Hackathon.Fiap.Domain.Usuarios.Entidades;

namespace Hackathon.Fiap.API.Controllers.Pacientes
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController(IPacientesAppServico pacientesAppServico) : ControllerBase
    {
        /// <summary>
        /// Recuperar pacientes com paginação.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = Roles.Medico)]
        public ActionResult<PaginacaoConsulta<PacienteResponse>> RecuperarPacientes([FromQuery] UsuarioListarRequest request)
        {
            PaginacaoConsulta<PacienteResponse> usuarios = pacientesAppServico.ListarPacientes(request);
            return Ok(usuarios);
        }
    }
}