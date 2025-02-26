using MediatR;

namespace CSharpApp.Application.Commands.Product
{
    public record CreateProductCommand(Core.Dtos.Product Product) : IRequest<Core.Dtos.Product>;
}
