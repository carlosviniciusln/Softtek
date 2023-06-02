using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Responses;
using Questao5.Domain.Handlers;

namespace Questao5.Application.Services.Controllers
{
    [ApiController]
    [Route("v1/movimentacoes")]
    public class MovimentacaoController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(RegistraMovimentacaoResponse), 200)]
        [ProducesResponseType(typeof(ErrorHandler), 400)]
        public Task<RegistraMovimentacaoResponse> Register(
            [FromServices] IMediator mediator,
            [FromBody] RegistraMovimentacaoRequest command
            )
        {
            return mediator.Send(command);
        }
    }
}