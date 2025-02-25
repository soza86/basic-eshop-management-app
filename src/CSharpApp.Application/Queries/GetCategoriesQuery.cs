using MediatR;

namespace CSharpApp.Application.Queries
{
    public record GetCategoriesQuery() : IRequest<IReadOnlyCollection<Category?>>;
}
