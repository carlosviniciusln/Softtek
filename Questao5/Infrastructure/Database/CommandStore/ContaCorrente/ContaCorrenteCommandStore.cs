using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Commands.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore.ContaCorrente
{
    public class ContaCorrenteCommandStore : IContaCorrenteCommandStore
    {
        private readonly DatabaseConfig _databaseConfig;

        public ContaCorrenteCommandStore(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<ResponseContaCorrenteCommand> GetByIdAsync(string id)
        {
            using var dbConnection = new SqliteConnection(_databaseConfig.Name);
            string query = "SELECT * FROM [contacorrente] WHERE idcontacorrente = @Id";
            var result = await dbConnection.QueryFirstOrDefaultAsync<ResponseContaCorrenteCommand>(query, new { Id = id });
            dbConnection.Close();
            return result;
        }
    }
}
