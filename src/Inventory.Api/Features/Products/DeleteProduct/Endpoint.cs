using FastEndpoints;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Features.Products.DeleteProduct;

[HttpDelete("products/{id:int}"), AllowAnonymous]
public sealed class Endpoint : Endpoint<Request>
{
    private readonly IProductService _productService;

    public Endpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var deleted = await _productService.DeleteAsync(req.Id);
        if (!deleted)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
