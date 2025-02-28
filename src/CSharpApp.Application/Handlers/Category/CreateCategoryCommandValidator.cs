using CSharpApp.Application.Commands.Category;
using FluentValidation;

namespace CSharpApp.Application.Handlers.Category
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Category)
                .NotNull()
                .WithMessage("Category request is invalid");

            RuleFor(x => x.Category.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Category.Image)
                .NotEmpty()
                .WithMessage("Image is required.");
        }
    }
}
