using System.Diagnostics.CodeAnalysis;

namespace Hackathon.Fiap.Domain.Utils.Excecoes
{
    [ExcludeFromCodeCoverage]
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
