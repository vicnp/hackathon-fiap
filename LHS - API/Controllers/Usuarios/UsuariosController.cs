using TC_Application.Usuarios.Interfaces;
using TC_DataTransfer.Usuarios.Request;
using TC_DataTransfer.Usuarios.Response;
using TC_Domain.Usuarios.Entidades;
using TC_IOT.Bibliotecas;
using Microsoft.AspNetCore.Mvc;

namespace LHS___API.Controllers.HealthCheck
{
    [Route ("api/usuarios")]
    [ApiController]
    public class UsuariosController (IUsuariosAppServico usuariosAppServico): ControllerBase
    {
        [HttpGet]
        public ActionResult<PaginacaoConsulta<UsuarioResponse>> RecuperarUsuarios([FromQuery] UsuarioListarRequest request)
        {
            PaginacaoConsulta<UsuarioResponse> usuarios = usuariosAppServico.ListarUsuarios(request);
            return Ok(usuarios);
        }
    }
}
