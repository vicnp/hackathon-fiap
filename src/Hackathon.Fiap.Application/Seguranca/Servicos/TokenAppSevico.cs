using Hackathon.Fiap.Application.Seguranca.Interfaces;
using Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces;

namespace Hackathon.Fiap.Application.Seguranca.Servicos
{
    public class TokenAppSevico(ITokenServico tokenServico) : ITokenAppSevico
    {
        public string GetToken(string identificador, string senha)
        {
            var token = tokenServico.GetToken(identificador, senha);
            return token;
        }
    }
}
