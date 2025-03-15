namespace Hackathon.Fiap.Application.Seguranca.Interfaces
{
    public interface ITokenAppSevico
    {
        Task<string> GetTokenAsync(string identificador, string senha, CancellationToken ct);
    }
}
