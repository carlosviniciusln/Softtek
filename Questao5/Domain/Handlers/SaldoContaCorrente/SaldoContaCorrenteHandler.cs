using MediatR;
using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Response;
using Questao5.Domain.Commands.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Validations;
using Questao5.Infrastructure.Database.CommandStore.ContaCorrente;
using Questao5.Infrastructure.Database.CommandStore.Movimento;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Questao5.Domain.Handlers.SaldoContaCorrente
{
    public class SaldoContaCorrenteHandler : RequestSaldoContaCorrenteCommand, IRequestHandler<RequestSaldoContaCorrenteCommand, string>
    {
        private readonly RulesValidation _validation = new RulesValidation();

        private readonly IMovimentoCommandStore _dbMovimentoCommand;
        private readonly IContaCorrenteCommandStore _dbContaCorrenteCommand;
        public SaldoContaCorrenteHandler(IMovimentoCommandStore dbMovimentoCommand, IContaCorrenteCommandStore dbContaCorrenteCommand)
        {
            _dbContaCorrenteCommand = dbContaCorrenteCommand;
            _dbMovimentoCommand = dbMovimentoCommand;
        }

        public async Task<string> Handle(RequestSaldoContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            var account = await getContaCorrenteAsync(request.IdContaCorrente);
            var movements = new RequestAccountMovementCommand { IdConta = request.IdContaCorrente };
            /*1. Validations*/
            if (_validation.IsValidForSaldoContaCorrente(account))
            {
                /*2. Get all movements from account*/
                var allMovements = getAllMovements(movements);
                var saldo = calculateTransactions(allMovements.Result);
                return await Task.FromResult(convertJson(buildResponse(request, account, saldo)));
            }
            else
            {
                throw new Exception("ValidationNotMapped");
            }
        }

        #region private functions
        private static ResponseSaldoContaCorrenteCommand buildResponse(RequestSaldoContaCorrenteCommand request, ResponseContaCorrenteCommand account, decimal saldo)
        {
            return new ResponseSaldoContaCorrenteCommand
            {
                DataHora = DateTime.Now,
                Saldo = saldo.ToString("F2"),
                IdContaCorrente = request.IdContaCorrente,
                Nome = account.Nome
            };
        }
        private string convertJson(ResponseSaldoContaCorrenteCommand response)
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

        private async Task<IEnumerable<ResponseMovimentoCommand>> getAllMovements(RequestAccountMovementCommand request)
        {
            return await Task.FromResult(await _dbMovimentoCommand.GetAllAsync(request));
        }

        private decimal calculateTransactions(IEnumerable<ResponseMovimentoCommand> allMovements)
        {
            decimal credito = 0;
            decimal debito = 0;

            foreach (var movement in allMovements)
            {
                if (movement.TipoMovimento.Equals(TipoMovimentacaoEnum.C.ToString()))
                {
                    credito += movement.Valor;
                }
                else
                {
                    debito += movement.Valor;
                }

            }
            return credito - debito;
        }
        #endregion
    }
}
