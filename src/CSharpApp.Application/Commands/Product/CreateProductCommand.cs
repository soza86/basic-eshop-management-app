using MediatR;

namespace CSharpApp.Application.Commands.Product
{
    public record CreateProductCommand(CreateProduct Product) : IRequest<Core.Dtos.Product>;
}
