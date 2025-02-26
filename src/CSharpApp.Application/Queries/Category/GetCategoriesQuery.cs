using MediatR;

namespace CSharpApp.Application.Queries.Category
{
    public record GetCategoriesQuery() : IRequest<List<Core.Dtos.Category>>;
}
