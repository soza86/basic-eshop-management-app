using MediatR;

namespace CSharpApp.Application.Commands.Category
{
    public record CreateCategoryCommand(CreateCategory Category) : IRequest<Core.Dtos.Category>;
}
