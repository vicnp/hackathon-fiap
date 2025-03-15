using Hackathon.Fiap.Application.Seguranca.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Hackathon.Fiap.API.Controllers.Seguranca
{
    [ApiController]
    [Route("/auth")]
    public class SegurancaController(ITokenAppSevico tokenAppSevico) : ControllerBase
    {
        /// <summary>
        /// Recuperar o token JWT.
        /// </summary>
        /// <param name="identificador"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Autenticar(string identificador, string senha)
        {
            string token = tokenAppSevico.GetToken(identificador, senha);
            if (token.IsNullOrEmpty())
            {
                return Unauthorized("Usuário ou senha errados.");
            }
            return Ok(token);
        }
    }
}

