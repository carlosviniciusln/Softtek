using MediatR;
using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Response;
using Questao5.Domain.Commands.Responses;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.CommandStore.ContaCorrente;
using Questao5.Infrastructure.Database.CommandStore.Idempotencia;
using Questao5.Infrastructure.Database.CommandStore.Movimento;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Questao5.Domain.Handlers.Movimento
{
    public class ContaCorrenteHandler : RequestAccountMovementCommand, IRequestHandler<RequestAccountMovementCommand, string>
    {
        private readonly RulesValidation _validation = new RulesValidation();

        private readonly IIdempotenciaCommandStore _dbIdempotenciaCommand;
        private readonly IMovimentoCommandStore _dbMovimentoCommand;
        private readonly IContaCorrenteCommandStore _dbContaCorrenteCommand;
        public ContaCorrenteHandler(IIdempotenciaCommandStore dbIdempotenciaRepository, IMovimentoCommandStore dbMovimentoCommand, IContaCorrenteCommandStore dbContaCorrenteCommand)
        {
            _dbContaCorrenteCommand = dbContaCorrenteCommand;
            _dbIdempotenciaCommand = dbIdempotenciaRepository;
            _dbMovimentoCommand = dbMovimentoCommand;
        }

        public async Task<string> Handle(RequestAccountMovementCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseAccountMovementCommand();
            response.Id = Guid.NewGuid();

            /*1. check if idempotencia exists*/
            var previousResult = await _dbIdempotenciaCommand.GetByIdAsync(request.IdIdempotencia);

            /*2. if previousResult, return this result*/
            if (previousResult == null)
            {
                var account = await getContaCorrenteAsync(request.IdConta);
                /*3. Validations*/
                if (_validation.IsValidForAccountMovement(request, account))
                {
                    addMovement(buildMovement(request, response));
                    addIdempotenciaAsync(convertIdempotencia(request, response));
                    return await Task.FromResult(convertJson(response));
                }
                else
                {
                    throw new Exception("ValidationNotMapped");
                }
            }
            else
            {
                response.Id = Guid.Parse(previousResult.Resultado);
                return await Task.FromResult(convertJson(response));
            }
        }

        #region private functions
        ///<summary>
        ///Validate idempotencia
        /// </summary>
        private async void addIdempotenciaAsync(ResponseIdempotenciaCommand request)
        {
            await _dbIdempotenciaCommand.AddAsync(request);
        }
        private static ResponseMovimentoCommand buildMovement(RequestAccountMovementCommand request, ResponseAccountMovementCommand response)
        {
            var movimento = new ResponseMovimentoCommand
            {
                IdMovimento = response.Id.ToString(),
                IdContaCorrente = request.IdConta ,
                DataMovimento = DateTime.Now.ToString(),
                TipoMovimento = request.Tipo,
                Valor = request.Valor
            };
            return movimento;
        }

        private static ResponseIdempotenciaCommand convertIdempotencia(RequestAccountMovementCommand request, ResponseAccountMovementCommand response2)
        {
            var jsonRequest = JsonSerializer.Serialize(request);

            return new ResponseIdempotenciaCommand
            {
                Chave_Idempotencia = request.IdIdempotencia,
                Requisicao = jsonRequest,
                Resultado = response2.Id.ToString()
            };
        }

        private async void addMovement(ResponseMovimentoCommand request)
        {
            await _dbMovimentoCommand.AddAsync(request);
        }

        private string convertJson(ResponseAccountMovementCommand response)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(response, jsonOptions);
            return json;
        } 

        private async Task<ResponseContaCorrenteCommand> getContaCorrenteAsync(string idConta)
        {
            return await _dbContaCorrenteCommand.GetByIdAsync(idConta.ToUpper());
        }
        #endregion
    }
}
