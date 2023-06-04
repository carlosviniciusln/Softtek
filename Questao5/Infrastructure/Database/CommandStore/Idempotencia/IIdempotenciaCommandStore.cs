using Questao5.Domain.Commands.Responses;

namespace Questao5.Infrastructure.Database.CommandStore.Idempotencia
{
    public interface IIdempotenciaCommandStore
    {
        Task<ResponseIdempotenciaCommand> GetByIdAsync(string id);
        Task AddAsync(ResponseIdempotenciaCommand request);
    }
}
