using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Commands.Responses
{
    public class ResponseSaldoContaCorrenteCommand
    {
        public string IdContaCorrente { get; set; }
        public string Nome { get; set; }
        public DateTime DataHora { get; set; }
        [DataType(DataType.Currency)]
        public decimal Saldo { get; set; }
    }
}
