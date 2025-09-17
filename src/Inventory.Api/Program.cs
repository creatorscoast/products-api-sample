
using FastEndpoints;
using FastEndpoints.Swagger;
using Inventory.Api.Database;
using Inventory.Api.Services;
using Inventory.Api.Services.Impl;

namespace Inventory.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddAuthorization()
            .AddFastEndpoints()
            .SwaggerDocument();

        builder.AddNpgsqlDataSource("inventorydb");

        builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();
        builder.Services.AddSingleton<DatabaseInitializer>();
        builder.Services.AddScoped<IProductService, ProductService>();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseFastEndpoints()
           .UseSwaggerGen();

        var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
            await databaseInitializer.InitializeAsync();

        app.Run();
    }
}
