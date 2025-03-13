namespace Seguranca.Servicos.Interfaces
{
    public interface ITokenServico
    {
        string GetToken(string email, string senha);
    }
}
