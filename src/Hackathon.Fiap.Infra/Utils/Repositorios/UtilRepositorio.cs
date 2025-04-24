using Hackathon.Fiap.Domain.Utils.Repositorios;
using Microsoft.Extensions.Configuration;

namespace Hackathon.Fiap.Infra.Utils.Repositorios
{
    public class UtilRepositorio : IUtilRepositorio
    {
        public string? GetValueConfigurationHash(IConfiguration configuration)
        {
            return configuration.GetValue<string>("SeedHash");
        }

        public string? GetValueConfigurationKeyJWT(IConfiguration configuration)
        {
            return configuration.GetValue<string>("SecretJWT");
        }
    }
}