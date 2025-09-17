using FastEndpoints;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Inventory.Api.Features.Products.GetProduct;

[HttpGet("products"), AllowAnonymous]
public sealed class GetAllEndpoint : EndpointWithoutRequest<IEnumerable<Response>,Mapper>
{
    private readonly IProductService _productService;

    public GetAllEndpoint(IProductService productService)
    {
        _productService = productService;
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _productService.GetAllAsync();

        Response = result?.Select(s=> Map.FromEntity(s))
            .ToList() ?? Enumerable.Empty<Response>();
    }
}
