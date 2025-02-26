using MediatR;

namespace CSharpApp.Application.Queries.Category
{
    public record GetCategoriesQuery() : IRequest<IReadOnlyCollection<Core.Dtos.Category?>>;
}
