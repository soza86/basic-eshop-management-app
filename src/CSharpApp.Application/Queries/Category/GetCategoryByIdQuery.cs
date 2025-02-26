using MediatR;

namespace CSharpApp.Application.Queries.Category
{
    public record GetCategoryByIdQuery(int Id) : IRequest<Core.Dtos.Category?>;
}
