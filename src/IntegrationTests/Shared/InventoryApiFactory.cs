using Inventory.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Shared;

public class InventoryApiFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public InventoryApiFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            var overrides = new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnectionString"] = _connectionString
            };
            config.AddInMemoryCollection(overrides!);
        });
    }
}
