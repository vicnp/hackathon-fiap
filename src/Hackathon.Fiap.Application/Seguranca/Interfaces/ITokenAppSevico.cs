namespace Seguranca.Interfaces
{
    public interface ITokenAppSevico
    {
        string GetToken(string identificador, string senha);
    }
}
