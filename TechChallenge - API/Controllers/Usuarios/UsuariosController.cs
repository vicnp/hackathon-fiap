using TC_Application.Usuarios.Interfaces;
using TC_DataTransfer.Usuarios.Request;
using TC_DataTransfer.Usuarios.Response;
using TC_IOC.Bibliotecas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TC_Domain.Utils;

namespace TC_API.Controllers.HealthCheck
{
    [Route ("api/usuarios")]
    [ApiController]
    public class UsuariosController (IUsuariosAppServico usuariosAppServico): ControllerBase
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
