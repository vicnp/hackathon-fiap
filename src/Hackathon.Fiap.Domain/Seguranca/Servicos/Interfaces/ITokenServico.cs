namespace Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces
{
    public interface ITokenServico
    {
        Task<string> GetTokenAsync(string email, string senha, CancellationToken ct);
    }
}
