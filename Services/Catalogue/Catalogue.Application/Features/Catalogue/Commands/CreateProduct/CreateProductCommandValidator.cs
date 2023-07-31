using FluentValidation;

namespace Catalogue.Application.Features.Catalogue.Commands.CreateProduct;


public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);;
        RuleFor(x => x.Prices).NotEmpty();
        RuleFor(x => x.Stock).NotEmpty();
    }
}