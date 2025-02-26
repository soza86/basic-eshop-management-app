using MediatR;

namespace CSharpApp.Application.Commands.Category
{
    public record CreateCategoryCommand(Core.Dtos.Category Category) : IRequest<Core.Dtos.Category>;
}
