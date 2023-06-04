using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Response;

namespace Questao5.Infrastructure.Database.CommandStore.Movimento
{
    public interface IMovimentoCommandStore
    {
        Task AddAsync(ResponseMovimentoCommand request);
        Task<IEnumerable<ResponseMovimentoCommand>> GetAllAsync(RequestAccountMovementCommand request);
    }
}
