using LHS_Application.Usuarios.Interfaces;
using LHS_DataTransfer.Usuarios.Request;
using LHS_DataTransfer.Usuarios.Response;
using LHS_Domain.Usuarios.Entidades;
using LHS_IOT.Bibliotecas;
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
