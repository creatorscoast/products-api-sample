using FastEndpoints;
using FluentValidation;

namespace Inventory.Api.Features.Products.CreateProduct;

public sealed class RequestValidator : Validator<Request>
{
    public RequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(75);
    }
}
