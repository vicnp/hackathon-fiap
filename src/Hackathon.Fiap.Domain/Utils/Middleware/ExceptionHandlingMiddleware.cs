using System.Net;
using System.Text.Json;
using Hackathon.Fiap.Domain.Utils.Excecoes;
using Microsoft.AspNetCore.Http;

namespace Hackathon.Fiap.Domain.Utils.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                RegraDeNegocioExcecao => HttpStatusCode.BadRequest,
                RequestInvalidoExcecao => HttpStatusCode.BadRequest,
                NaoAutorizadoExcecao => HttpStatusCode.Unauthorized,
                KeyNotFoundException => HttpStatusCode.NotFound,
                FalhaConversaoExcecao => HttpStatusCode.InternalServerError,
                ErroInternoExcecao => HttpStatusCode.InternalServerError,
                RegistroNaoEncontradoExcecao => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                Erro = new
                {
                    Mensagem = exception.Message,
                    Tipo = exception.GetType().Name,
                    response.StatusCode
                }
            });

            return response.WriteAsync(result);
        }
    }
}