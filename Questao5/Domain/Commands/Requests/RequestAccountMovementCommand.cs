using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Commands.Requests
{
    public class RequestAccountMovementCommand: IRequest<string>
    {
        public string IdIdempotencia { get; set; }
        public string IdConta { get; set; }
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
    }
}