using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Commands.Requests;
using Questao5.Domain.Commands.Response;
using Questao5.Domain.Commands.Responses;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Infrastructure.Database.CommandStore.Movimento
{
    public class MovimentoCommandStore : IMovimentoCommandStore
    {
        private readonly DatabaseConfig _databaseConfig;

        public MovimentoCommandStore(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public async Task AddAsync(ResponseMovimentoCommand request)
        {
            using var dbConnection = new SqliteConnection(_databaseConfig.Name);
            string query = "INSERT INTO [movimento] (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) " +
                           "VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor)";
            await dbConnection.ExecuteAsync(query,
                new
                {
                    IdMovimento = request.IdMovimento,
                    IdContaCorrente = request.IdContaCorrente,
                    DataMovimento = request.DataMovimento,
                    TipoMovimento = request.TipoMovimento,
                    Valor = request.Valor
                });
            dbConnection.Close();
        }

        public async Task<IEnumerable<ResponseMovimentoCommand>> GetAllAsync(RequestAccountMovementCommand request)
        {
            using var dbConnection = new SqliteConnection(_databaseConfig.Name);
            string query = "SELECT m.*, c.* FROM [movimento] as m " +
                           "INNER JOIN [contacorrente] as c " +
                           "on m.idcontacorrente = c.idcontacorrente " +
                           "WHERE m.idcontacorrente = @Id";
            var result = await dbConnection.QueryAsync<ResponseMovimentoCommand, ResponseContaCorrenteCommand, ResponseMovimentoCommand>(query,
            (movimento, contaCorrente) =>
            {
                movimento.ContaCorrente = contaCorrente;
                return movimento;
            },
            new { Id = request.IdConta },
            splitOn: "IdContaCorrente"); ;
            dbConnection.Close();
            return result;
        }
    }
}
