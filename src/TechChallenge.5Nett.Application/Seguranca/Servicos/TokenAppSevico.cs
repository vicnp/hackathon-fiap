using Seguranca.Interfaces;
using Seguranca.Servicos.Interfaces;

namespace Seguranca.Servicos
{
    public class TokenAppSevico(ITokenServico tokenServico) : ITokenAppSevico
    {
        public string GetToken(string email, string senha)
        {
            var token = tokenServico.GetToken(email, senha);
            return token;
        }
    }
}
