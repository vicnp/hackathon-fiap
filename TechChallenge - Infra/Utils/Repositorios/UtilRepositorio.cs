using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC_Domain.Utils.Repositorios;

namespace TC_Infra.Utils.Repositorios
{
    public class UtilRepositorio : IUtilRepositorio
    {
        public string? GetValueConfigurationHash(IConfiguration configuration)
            => configuration.GetValue<string>("SeedHash");

        public string? GetValueConfigurationKeyJWT(IConfiguration configuration)
            => configuration.GetValue<string>("SecretJWT");
    }
}
