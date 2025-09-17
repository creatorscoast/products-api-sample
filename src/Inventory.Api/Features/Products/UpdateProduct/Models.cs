using System.Text.Json.Serialization;

namespace Inventory.Api.Features.Products.UpdateProduct;

public class Request
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class Response : ProductModel { }
