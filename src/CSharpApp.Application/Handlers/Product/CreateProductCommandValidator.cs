using CSharpApp.Application.Commands.Product;
using FluentValidation;

namespace CSharpApp.Application.Handlers.Product
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product)
                .NotNull()
                .WithMessage("Product request is invalid");

            RuleFor(x => x.Product.Price)
                .NotEmpty()
                .WithMessage("Price is required.")
                .GreaterThan(0)
                .WithMessage("Price should greater than zero.");

            RuleFor(x => x.Product.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(x => x.Product.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.Product.CategoryId)
                .NotEmpty()
                .WithMessage("Category Id is required.");
        }
    }
}
