using Seguranca.Interfaces;
using Seguranca.Servicos.Interfaces;

namespace Seguranca.Servicos
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
