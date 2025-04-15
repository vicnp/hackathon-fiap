namespace Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces
{
    public interface ITokenServico
    {
        Task<string> GetTokenAsync(string identificador, string senha, CancellationToken ct);
    }
}
