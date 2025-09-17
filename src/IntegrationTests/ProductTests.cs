using IntegrationTests.Shared;
using Inventory.Api.Features.Products.CreateProduct;
using System.Net.Http.Json;

namespace IntegrationTests;

public class ProductEndpointsTests : IClassFixture<PostgresContainerFixture>, IAsyncLifetime
{
    private readonly PostgresContainerFixture _pg;
    private InventoryApiFactory? _factory;
    private HttpClient? _client;

    public ProductEndpointsTests(PostgresContainerFixture pg)
    {
        _pg = pg;
    }

    public async Task InitializeAsync()
    {
        _factory = new InventoryApiFactory(_pg.ConnectionString);
        _client = _factory.CreateClient();
        _ = await _client.GetAsync("/products");
    }

    public Task DisposeAsync()
    {
        _client?.Dispose();
        _factory?.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Create_Then_Get_Product_Works()
    {
        Assert.NotNull(_client);
        
        // Arrange
        var create = new Request
        {
            Name = "Test Widget",
            Price = 12.34m,
            Description = "A test product"
        };

        // Act
        var createResp = await _client!.PostAsJsonAsync("/products", create);
        createResp.EnsureSuccessStatusCode();

        // Assert
        var created = await createResp.Content.ReadFromJsonAsync<Inventory.Api.Features.Products.CreateProduct.Response>();
        Assert.NotNull(created);
        Assert.True(created!.Id > 0);
        Assert.Equal(create.Name, created.Name);

        var getResp = await _client.GetFromJsonAsync<Inventory.Api.Features.Products.GetProduct.Response>($"/products/{created.Id}");
        Assert.NotNull(getResp);
        Assert.Equal(created.Id, getResp!.Id);
        Assert.Equal(create.Name, getResp.Name);
    }

    [Fact]
    public async Task List_Products_Returns_Items()
    {
        Assert.NotNull(_client);

        // Arrange
        var create = new Request { Name = "Another", Price = 1m };
        
        // Act
        var resp = await _client!.PostAsJsonAsync("/products", create);
        resp.EnsureSuccessStatusCode();

        var list = await _client.GetFromJsonAsync<IEnumerable<Inventory.Api.Features.Products.GetProduct.Response>>("/products");
        
        // Assert
        Assert.NotNull(list);
        Assert.True(list!.Any());
    }
}
