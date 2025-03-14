using Hackathon.Fiap.Application.Usuarios.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController(IUsuariosAppServico usuariosAppServico) : ControllerBase
    {

    }
}
