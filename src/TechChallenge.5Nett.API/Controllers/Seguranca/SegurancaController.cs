using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Seguranca.Interfaces;

namespace Controllers.Seguranca
{
    [ApiController]
    [Route("/auth")]
    public class SegurancaController(ITokenAppSevico tokenAppSevico) : ControllerBase
    {
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

