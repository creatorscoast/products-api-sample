using System.Text.Json.Serialization;

namespace Inventory.Api.Features.Products;

public class ProductModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
