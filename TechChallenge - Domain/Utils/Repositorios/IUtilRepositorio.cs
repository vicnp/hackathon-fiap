using Microsoft.Extensions.Configuration;

namespace TC_Domain.Utils.Repositorios
{
    public interface IUtilRepositorio
    {
        string? GetValueConfigurationHash(IConfiguration configuration);

        string? GetValueConfigurationKeyJWT(IConfiguration configuration);
    }
}
