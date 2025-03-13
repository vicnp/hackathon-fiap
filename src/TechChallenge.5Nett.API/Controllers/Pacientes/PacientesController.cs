using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Usuarios.Entidades;
using Utils;
using Pacientes.Interfaces;
using Pacientes.Responses;
using Usuarios.Request;

namespace Controllers.Pacientes
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacientesController(IPacientesAppServico pacientesAppServico) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = Roles.Medico)]
        public ActionResult<PaginacaoConsulta<PacienteResponse>> RecuperarPacientes([FromQuery] UsuarioListarRequest request)
        {
            PaginacaoConsulta<PacienteResponse> usuarios = pacientesAppServico.ListarPacientes(request);
            return Ok(usuarios);
        }
    }
}