namespace Seguranca.Interfaces
{
    public interface ITokenAppSevico
    {
        string GetToken(string email, string senha);
    }
}
