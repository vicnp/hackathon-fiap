using Microsoft.Extensions.Configuration;

namespace Utils.Repositorios
{
    public class UtilRepositorio : IUtilRepositorio
    {
        public string? GetValueConfigurationHash(IConfiguration configuration)
            => configuration.GetValue<string>("SeedHash");

        public string? GetValueConfigurationKeyJWT(IConfiguration configuration)
            => configuration.GetValue<string>("SecretJWT");
    }
}