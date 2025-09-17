using FastEndpoints;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Features.Products.UpdateProduct;

[HttpPut("products/{id:int}"), AllowAnonymous]
public sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IProductService _productService;

    public Endpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var exitingProduct = await _productService.GetAsync(req.Id);

        if (exitingProduct is null)
        {
            await Send.NotFoundAsync(ct);

            return;
        }

        var product = Map.ToEntity(req);
        await _productService.UpdateAsync(product);

        await Send.OkAsync(Map.FromEntity(product), ct);
    }
}
