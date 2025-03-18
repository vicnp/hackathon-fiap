namespace Hackathon.Fiap.Domain.Utils.Excecoes
{
    public class FalhaConversaoExcecao : Exception
    {
        public FalhaConversaoExcecao()
        {
            
        }
        public FalhaConversaoExcecao(string? message) : base(message)
        {
        }
    }
}
