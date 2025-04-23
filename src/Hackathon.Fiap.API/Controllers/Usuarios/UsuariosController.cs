using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Hackathon.Fiap.DataTransfer.Usuarios.Request;
using Hackathon.Fiap.DataTransfer.Usuarios.Response;
using Hackathon.Fiap.Domain.Usuarios.Entidades;
using Hackathon.Fiap.Domain.Utils;
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
        public async Task<ActionResult<PaginacaoConsulta<UsuarioResponse>>> ListarUsuarios([FromQuery] UsuarioListarRequest request, CancellationToken ct)
        {
            PaginacaoConsulta<UsuarioResponse> paginacaoConsulta = await usuariosAppServico.ListarUsuariosAsync(request, ct);
            return Ok(paginacaoConsulta);
        }


        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> CadastraUsuarioAsync([FromBody] UsuarioCadastroRequest request, CancellationToken ct)
        {
            UsuarioResponse response = await usuariosAppServico.CadastrarUsuarioAsync(request, ct);
            return Ok(response);
        }
    }
}
