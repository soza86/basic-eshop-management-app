using MediatR;

namespace CSharpApp.Application.Commands
{
    public record CreateCategoryCommand(Category Category) : IRequest<Category>;
}
