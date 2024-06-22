using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Domain.Seguranca.Servicos.Interfaces
{
    public interface ITokenServico
    {
        string GetToken(string email, string senha);
    }
}
