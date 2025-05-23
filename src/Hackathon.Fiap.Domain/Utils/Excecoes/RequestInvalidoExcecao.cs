﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Hackathon.Fiap.Domain.Utils.Excecoes
{
    [ExcludeFromCodeCoverage]
    public class RequestInvalidoExcecao : Exception
    {
        public RequestInvalidoExcecao()
        {
            
        }
        public RequestInvalidoExcecao(string? message) : base(message)
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
            throw new RequestInvalidoExcecao(paramName);
        }
    }
}
