using MediatR;
using Questao5.Domain.Commands.Responses;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Commands.Requests
{
    public class RegistraMovimentacaoRequest: IRequest<RegistraMovimentacaoResponse>
    {
        public Guid? IdConta { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Valor { get; set; }
        public string? Tipo { get; set; }
    }
}