using Microsoft.AspNetCore.Mvc;
using Usuarios.Interfaces;

namespace Controllers.Usuarios
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController(IUsuariosAppServico usuariosAppServico) : ControllerBase
    {
       
    }
}
