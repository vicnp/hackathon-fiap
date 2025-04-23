namespace Hackathon.Fiap.Domain.Seguranca.Servicos.Interfaces
{
    public interface ITokenServico
    {
        string EncryptPassword(string password);
        Task<string> GetTokenAsync(string? identificador, string? senha, CancellationToken ct);
    }
}
