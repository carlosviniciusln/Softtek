using Questao5.Application.Configurations.Helpers;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Questao5.Domain.Handlers
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync<Exception>(context, ex);
            }
        }

        private async Task HandleExceptionAsync<TResponse>(HttpContext context, Exception ex) where TResponse : Exception
        {

            ErrorResponse response = BuildHttpErrorResponse(ex);
            context.Response.StatusCode = response.ErrorCode;

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(response, jsonOptions);


            await context.Response.WriteAsync(json);
        }

        private ErrorResponse BuildHttpErrorResponse(Exception ex)
        {
            ErrorResponse response = new ErrorResponse();
            CustomException customException = new CustomException();
            switch (ex)
            {
                case TipoMovimentacaoInvalidaException:
                    response = customException.TipoMovimentacaoInvalidaResponse();
                    break;
                case ValorInvalidoException:
                    response = customException.ValorInvalidoResponse();
                    break;
                case ContaMovimentacaoInvalidaException:
                    response = customException.ContaMovimentacaoInvalidaResponse();
                    break;
                case ContaNaoInformadaException:
                    response = customException.ContaNaoInformadaResponse();
                    break;
                case ValorNaoInformadoException:
                    response = customException.ValorNaoInformadoResponse();
                    break;
                case TipoNaoInformadoException:
                    response = customException.TipoNaoInformadoResponse();
                    break;
                default:
                    response = customException.ServerErrorResponse();
                    break;
            }
            return response;
        }
    }
}
