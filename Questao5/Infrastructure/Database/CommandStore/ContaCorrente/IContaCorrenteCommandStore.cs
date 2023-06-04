
using Questao5.Domain.Commands.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.ContaCorrente
{
    public interface IContaCorrenteCommandStore
    {
        Task<ResponseContaCorrenteCommand> GetByIdAsync(string id);
    }
}
