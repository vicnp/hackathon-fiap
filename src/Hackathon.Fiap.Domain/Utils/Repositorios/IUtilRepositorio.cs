using Microsoft.Extensions.Configuration;

namespace Utils.Repositorios
{
    public interface IUtilRepositorio
    {
        string? GetValueConfigurationHash(IConfiguration configuration);

        string? GetValueConfigurationKeyJWT(IConfiguration configuration);
    }
}
