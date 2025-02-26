using MediatR;

namespace CSharpApp.Application.Queries.Product
{
    public record GetProductsQuery() : IRequest<List<Core.Dtos.Product>>;
}
