using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Utils;
using Usuarios.Request;
using Usuarios.Response;
using Usuarios.Interfaces;
using Usuarios.Entidades;

namespace Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController(IUsuariosAppServico usuariosAppServico) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = $"{Roles.Paciente},{Roles.Medico}")]
        public ActionResult<PaginacaoConsulta<UsuarioResponse>> RecuperarPacientes([FromQuery] PacienteListarRequest request)
        {
            PaginacaoConsulta<UsuarioResponse> usuarios = usuariosAppServico.ListarPacientes(request);
            return Ok(usuarios);
        }
    }
}
