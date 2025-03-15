using Hackathon.Fiap.Application.Seguranca.Interfaces;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;

namespace Hackathon.Fiap.Application.Seguranca.Servicos
{
    public class TokenAppSevico(ITokenServico tokenServico) : ITokenAppSevico
    {
        public async Task<string> GetTokenAsync(string identificador, string senha, CancellationToken ct)
        {
            var token = await tokenServico.GetTokenAsync(identificador, senha, ct);
            return token;
        }
    }
}
