using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Inventory.Api.Features.Products.CreateProduct;

public class Request
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; } = 0;

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}

public class Response : ProductModel { }
