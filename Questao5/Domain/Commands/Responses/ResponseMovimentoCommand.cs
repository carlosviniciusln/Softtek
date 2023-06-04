
using Questao5.Domain.Commands.Responses;

namespace Questao5.Domain.Commands.Response

{
    public class ResponseMovimentoCommand
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public string DataMovimento { get; set; }
        public string TipoMovimento { get; set; }
        public decimal Valor { get; set; }
        public ResponseContaCorrenteCommand ContaCorrente { get; set; }
    }
}
