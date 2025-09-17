using FastEndpoints;
using Inventory.Api.Entities;

namespace Inventory.Api.Features.Products.UpdateProduct;

public class Mapper : Mapper<Request, Response, Product>
{
    public override Product ToEntity(Request r)
    {
        return new Product
        {
            Description = r.Description,
            Id = r.Id,
            Price = r.Price,
        };
    }

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
