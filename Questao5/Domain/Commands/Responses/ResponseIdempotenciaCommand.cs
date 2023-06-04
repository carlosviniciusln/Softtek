
namespace Questao5.Domain.Commands.Responses
{
    public class ResponseIdempotenciaCommand
    {
        public string Chave_Idempotencia { get; set; }
        public string Requisicao { get; set; }
        public string Resultado { get; set; }
    }
}
