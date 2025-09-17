using System.Text.Json.Serialization;

namespace Inventory.Api.Entities;

public class Product
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
