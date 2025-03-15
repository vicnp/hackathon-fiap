using Hackathon.Fiap.Application.Medicos.Servicos;
using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Hackathon.Fiap.DataTransfer.Medicos.Requests;
using Hackathon.Fiap.DataTransfer.Medicos.Responses;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.DataTransfer.Utils;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController(IUsuariosAppServico usuariosAppServico) : ControllerBase
    {
        /// <summary>
        /// Consulta de usuários, limitado ao administrador.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("paginados")]
        [Authorize(Roles = Roles.Administrador)]
        public ActionResult<PaginacaoConsulta<UsuarioResponse>> ListarUsuarios([FromQuery] UsuarioListarRequest request)
        {
            PaginacaoConsulta<UsuarioResponse> paginacaoConsulta = usuariosAppServico.ListarUsuarios(request);
            return Ok(paginacaoConsulta);
        }
    }
}
