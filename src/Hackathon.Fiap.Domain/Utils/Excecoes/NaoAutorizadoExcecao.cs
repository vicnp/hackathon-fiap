using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Hackathon.Fiap.Domain.Utils.Excecoes
{
    public class NaoAutorizadoExcecao : Exception
    {
        public NaoAutorizadoExcecao()
        {
            
        }
        public NaoAutorizadoExcecao(string? message) : base(message)
        {

        }

        public static void LancarExcecaoSeNulo([NotNull] object? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument is null)
            {
                Throw(paramName);
            }
        }

        [DoesNotReturn]
        internal static void Throw(string? paramName)
        {
            throw new NaoAutorizadoExcecao(paramName);
        }
    }
}
