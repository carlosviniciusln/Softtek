using MediatR;
using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Responses;
using Questao5.Domain.Validations;

namespace Questao5.Domain.Handlers
{
    public class RegistraMovimentacaoHandler : IRequestHandler<RegistraMovimentacaoRequest, RegistraMovimentacaoResponse>
    {
        private readonly RegistraMovimentacaoValidation _validation = new RegistraMovimentacaoValidation();

        public async Task<RegistraMovimentacaoResponse> Handle(RegistraMovimentacaoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (_validation.IsValid(request))
                {
                       
                }
                var idRequest = Guid.NewGuid();
                var response = new RegistraMovimentacaoResponse
                {
                    Id = idRequest
                };
                return await Task.FromResult(response);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
