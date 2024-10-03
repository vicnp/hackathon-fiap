using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Utils;
using Usuarios.Request;
using Usuarios.Response;
using Usuarios.Interfaces;

namespace Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController(IUsuariosAppServico usuariosAppServico) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "1")]
        public ActionResult<PaginacaoConsulta<UsuarioResponse>> RecuperarUsuarios([FromQuery] UsuarioListarRequest request)
        {
            PaginacaoConsulta<UsuarioResponse> usuarios = usuariosAppServico.ListarUsuarios(request);
            return Ok(usuarios);
        }
    }
}
