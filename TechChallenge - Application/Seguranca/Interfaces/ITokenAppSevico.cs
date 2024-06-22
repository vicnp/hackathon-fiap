using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC_Application.Seguranca.Interfaces
{
    public interface ITokenAppSevico
    {
        string GetToken(string email, string senha);
    }
}
