using FastEndpoints;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Features.Products.GetProduct;

[HttpGet("products/{id:int}"), AllowAnonymous]
public sealed class GetEndpoint : EndpointWithoutRequest<Response, Mapper>
{
    private readonly IProductService _productService;

    public GetEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");

        var product = await _productService.GetAsync(id);
        if (product is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(Map.FromEntity(product));
    }
}
