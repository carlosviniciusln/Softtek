using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Configurations.Helpers;
using Questao5.Domain.Commands.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Services.Controllers
{
    [ApiController]
    [Route("v1/saldos")]
    public class SaldosContaCorrenteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        #region Swagger
        [SwaggerOperation("API responsável por obter o saldo do responsavel da conta.")]
        [SwaggerResponse(200, CustomResponseMessages.Response1)]
        [SwaggerResponse(400, CustomResponseMessages.Response5)]
        [SwaggerResponse(404, CustomResponseMessages.Response3)]
        [SwaggerResponse(417, CustomResponseMessages.Response7)]
        [SwaggerResponse(500, CustomResponseMessages.Response6)] 
        #endregion
        public Task<string> GetSaldo(
            [FromServices] IMediator mediator,
            [FromBody] RequestSaldoContaCorrenteCommand command
            )
        {
            return mediator.Send(command);
        }
    }
}