using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Questao5.Domain.Handlers
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string message = "";

            if (ex.Message == "TipoMovimentacaoInvalidaException")
            {
                statusCode = HttpStatusCode.BadRequest;
                message = "Apenas movimentações C e D são permitidas.";
            }
            else if (ex.Message == "ValorInvalidoException")
            {
                statusCode = HttpStatusCode.NotAcceptable;
                message = "Informe um valor maior que 0 (zero) para uma transação.";
            }
            else if (ex.Message == "ContaMovimentacaoInvalidaException")
            {
                statusCode = HttpStatusCode.NotFound;
                message = "A conta informada não foi encontrada em nossa base de dados.";
            }
            else if (ex.Message == "IdContaException")
            {
                statusCode = HttpStatusCode.ExpectationFailed;
                message = "É necessário informar uma conta.";
            }
            else if (ex.Message == "ValorException")
            {
                statusCode = HttpStatusCode.ExpectationFailed;
                message = "É necessário informar uma valor.";
            }
            else if (ex.Message == "TipoException")
            {
                statusCode = HttpStatusCode.ExpectationFailed;
                message = "É necessário informar um tipo de transação.";
            }
            else
            {
                message = "Ocorreu um erro inesperado. Tente novamente mais tarde.";
            }

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = (int)statusCode;


            var response = new ErrorHandler((int)statusCode, message);

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(response, jsonOptions);

            await context.Response.WriteAsync(json);
        }
    }

    public class ErrorHandler {
        public int code { get; set; }
        public string message { get; set; }

        public ErrorHandler(int codigo, string mensagem)
        {
            code = codigo;
            message = mensagem;
        }
    }
}
