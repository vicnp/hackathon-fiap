using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace TC_Domain.Utils.Repositorios
{
    public interface IUtilRepositorio
    {
        [ExcludeFromCodeCoverage]
        string? GetValueConfigurationHash(IConfiguration configuration)
            => configuration.GetValue<string>("SeedHash");

        [ExcludeFromCodeCoverage]
        string? GetValueConfigurationKeyJWT(IConfiguration configuration)
            => configuration.GetValue<string>("SecretJWT");
    }
}
