using MediatR;

namespace CSharpApp.Application.Queries.Product
{
    public record GetProductsQuery() : IRequest<IReadOnlyCollection<Core.Dtos.Product?>>;
}
