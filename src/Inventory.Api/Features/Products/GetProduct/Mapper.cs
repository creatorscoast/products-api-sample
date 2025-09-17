using FastEndpoints;
using Inventory.Api.Entities;

namespace Inventory.Api.Features.Products.GetProduct;

public class Mapper : Mapper<dynamic, Response, Product>
{
    public override Response FromEntity(Product e)
    {
        return new Response
        {
            Id = e.Id,
            Description = e.Description,
            Name = e.Name,
            Price = e.Price
        };
    }
}
