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

        /// <summary>
        /// Entrada para todas as requisições HTTP
        /// </summary>
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

        /// <summary>
        /// Tratamento de exceptions com retorno em json indentado
        /// </summary>
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

        /// <summary>
        /// Constroi as mensagens personalizadas para cada tipo de erro.
        /// </summary>
        private ErrorResponse BuildHttpErrorResponse(Exception ex)
        {
            switch (ex.Message)
            {
                case "TipoMovimentacaoInvalidaException":
                    return new ErrorResponse((int)HttpStatusCode.BadRequest, CustomResponseMessages.Response2, "INVALID_TYPE");
                case "ContaMovimentacaoInvalidaException":
                    return new ErrorResponse((int)HttpStatusCode.NotFound, CustomResponseMessages.Response3, "INVALID_ACCOUNT");
                case "ValorInvalidoException":
                    return new ErrorResponse((int)HttpStatusCode.NotAcceptable, CustomResponseMessages.Response4, "INVALID_VALUE");
                case "ContaMovimentacaoInativaException":
                    return new ErrorResponse((int)HttpStatusCode.BadRequest, CustomResponseMessages.Response5, "INACTIVE_ACCOUNT");
                case "ValidationNotMapped":
                    return new ErrorResponse((int)HttpStatusCode.ExpectationFailed, ex.Message, "SERVER_ERROR");
                default:
                    return new ErrorResponse((int)HttpStatusCode.InternalServerError, ex.Message, "SERVER_ERROR");
            }
        }
    }
}
