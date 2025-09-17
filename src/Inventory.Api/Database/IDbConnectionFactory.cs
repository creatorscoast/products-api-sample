using Npgsql;
using System.Data;

namespace Inventory.Api.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync();
}

public sealed class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly NpgsqlDataSource _dataSource = null;
    private readonly string _connectionString = null;

    public NpgsqlConnectionFactory(NpgsqlDataSource npgsqlDataSource)
    {
       _dataSource = npgsqlDataSource;
    }

    public NpgsqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        if (_dataSource is null)
        {
            var connection = new NpgsqlConnection(_connectionString);

            await connection.OpenAsync();

            return connection;
        }

        var sourceConnection = await _dataSource.OpenConnectionAsync();

        return sourceConnection;
    }
}