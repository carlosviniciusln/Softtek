using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Commands.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore.Idempotencia
{
    public class IdempotenciaCommandStore : IIdempotenciaCommandStore
    {
        private readonly DatabaseConfig _databaseConfig;

        public IdempotenciaCommandStore(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ResponseIdempotenciaCommand> GetByIdAsync(string id)
        {
            using var dbConnection = new SqliteConnection(_databaseConfig.Name);
            string query = "SELECT * FROM idempotencia WHERE chave_idempotencia = @Id";
            var result = await dbConnection.QueryFirstOrDefaultAsync<ResponseIdempotenciaCommand>(query, new { Id = id });
            dbConnection.Close();
            return result;
        }

        public async Task AddAsync(ResponseIdempotenciaCommand request)
        {
            using var dbConnection = new SqliteConnection(_databaseConfig.Name);
            string query = "INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@IdIdempotencia, @Requisicao, @Resultado)";
            await dbConnection.ExecuteAsync(query, new { IdIdempotencia = request.Chave_Idempotencia, Requisicao = request.Requisicao, Resultado = request.Resultado});
            dbConnection.Close();
        }
    }
}
