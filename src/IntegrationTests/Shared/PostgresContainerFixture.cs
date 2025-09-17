using Inventory.Api.Database;
using Testcontainers.PostgreSql;

namespace IntegrationTests.Shared;

public sealed class PostgresContainerFixture : IAsyncLifetime
{
    public PostgreSqlContainer Container { get; } = new PostgreSqlBuilder()
        .WithDatabase("inventory")
        .WithUsername("postgres")
        .WithPassword("password")
        .WithImage("postgres:17")
        .Build();

    public string ConnectionString => Container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await Container.StartAsync();

        var dbFactory = new NpgsqlConnectionFactory(ConnectionString);
        await new DatabaseInitializer(dbFactory)
            .InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }
}
