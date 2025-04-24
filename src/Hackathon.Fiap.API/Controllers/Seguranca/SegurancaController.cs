using Hackathon.Fiap.Application.Seguranca.Interfaces;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Hackathon.Fiap.Domain.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon.Fiap.API.Controllers.Seguranca
{
    [ApiController]
    [Route("api/auth")]
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
                throw new NaoAutorizadoExcecao("Usuario ou senha errados.");
            }
            return Ok(token);
        }
    }
}

