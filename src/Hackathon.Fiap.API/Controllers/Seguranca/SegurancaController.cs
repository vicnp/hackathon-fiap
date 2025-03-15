using Hackathon.Fiap.Application.Seguranca.Interfaces;
using Hackathon.Fiap.Domain.Utils;
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
        public async Task<IActionResult> Autenticar(string identificador, string senha, CancellationToken ct)
        {
            string token = await tokenAppSevico.GetTokenAsync(identificador, senha, ct);
            if (token.InvalidOrEmpty())
            {
                return Unauthorized("Usuário ou senha errados.");
            }
            return Ok(token);
        }
    }
}

