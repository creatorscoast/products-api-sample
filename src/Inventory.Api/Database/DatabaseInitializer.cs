using Dapper;

namespace Inventory.Api.Database;

public class DatabaseInitializer
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DatabaseInitializer(IDbConnectionFactory dbConnectionFactory)
    {
        _connectionFactory = dbConnectionFactory;
    }

    public async Task InitializeAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        // Create products table
        await connection.ExecuteAsync(@"
        CREATE TABLE IF NOT EXISTS products (
            id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            name VARCHAR(75) NOT NULL,
            description VARCHAR(250),
            price DECIMAL(18,2) NOT NULL DEFAULT 0,
            createdAt TIMESTAMP NOT NULL DEFAULT NOW(),
            updatedAt TIMESTAMP NOT NULL DEFAULT NOW()
        );
    ");
    }
}
