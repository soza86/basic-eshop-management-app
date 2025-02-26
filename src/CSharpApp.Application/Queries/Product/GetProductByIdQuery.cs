using MediatR;

namespace CSharpApp.Application.Queries.Product
{
    public record GetProductByIdQuery(int Id) : IRequest<Core.Dtos.Product?>;
}
