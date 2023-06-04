using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Configurations.Helpers;
using Questao5.Domain.Commands.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Questao5.Application.Services.Controllers
{
    [ApiController]
    [Route("v1/movimentacoes")]
    public class MovimentacoesController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        #region Swagger
        [SwaggerOperation("API responsável por registrar movimentações de contas.")]
        [SwaggerResponse(200, CustomResponseMessages.Response1)]
        [SwaggerResponse(400, CustomResponseMessages.Response2)]
        [SwaggerResponse(400, CustomResponseMessages.Response5)]
        [SwaggerResponse(404, CustomResponseMessages.Response3)]
        [SwaggerResponse(406, CustomResponseMessages.Response4)]
        [SwaggerResponse(417, CustomResponseMessages.Response6)]
        [SwaggerResponse(500, CustomResponseMessages.Response6)] 
        #endregion
        public Task<string> Register(
            [FromServices] IMediator mediator,
            [FromBody] RequestAccountMovementCommand command
            )
        {
            return mediator.Send(command);
        }
    }
}