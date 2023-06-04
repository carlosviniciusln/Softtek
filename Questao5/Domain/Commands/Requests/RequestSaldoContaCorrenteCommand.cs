
using MediatR;

namespace Questao5.Domain.Commands.Requests
{
    public class RequestSaldoContaCorrenteCommand : IRequest<string>
    {
        public string IdContaCorrente { get; set; }
    }
}
