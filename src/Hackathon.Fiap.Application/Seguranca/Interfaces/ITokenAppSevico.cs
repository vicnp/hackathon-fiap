namespace Hackathon.Fiap.Application.Seguranca.Interfaces
{
    public interface ITokenAppSevico
    {
        string GetToken(string identificador, string senha);
    }
}
