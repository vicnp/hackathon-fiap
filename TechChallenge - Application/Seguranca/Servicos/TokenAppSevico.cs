using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Application.Seguranca.Interfaces;
using TC_Domain.Seguranca.Servicos.Interfaces;

namespace TC_Application.Seguranca.Servicos
{
    public  class TokenAppSevico(ITokenServico tokenServico) : ITokenAppSevico
    {
        public string GetToken(string email, string senha)
        {
            var token = tokenServico.GetToken(email, senha);
            return token;
        }
    }
}
