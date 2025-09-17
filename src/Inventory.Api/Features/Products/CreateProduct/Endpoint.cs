using FastEndpoints;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Features.Products.CreateProduct;

[HttpPost("products"), AllowAnonymous]
public sealed class Endpoint : Endpoint<Request, Response, Mapper>
{
    private readonly IProductService _productService;

    public Endpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var entity = Map.ToEntity(req);

        entity.Id = await _productService.CreateAsync(entity) ?? 0;

        await Send.CreatedAtAsync($"/products/{entity.Id}",
            new { id = entity.Id },
            Map.FromEntity(entity),
            generateAbsoluteUrl: true);
    }
}
