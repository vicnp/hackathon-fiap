using Microsoft.Extensions.Configuration;

namespace Hackathon.Fiap.Domain.Utils.Repositorios
{
    public interface IUtilRepositorio
    {
        string? GetValueConfigurationHash(IConfiguration configuration);

        string? GetValueConfigurationKeyJWT(IConfiguration configuration);
    }
}
