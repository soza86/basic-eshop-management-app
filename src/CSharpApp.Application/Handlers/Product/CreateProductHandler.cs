using CSharpApp.Application.Commands.Product;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Core.Dtos.Product>
    {
        private readonly IProductsService _productsService;

        public CreateProductHandler(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<Core.Dtos.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productsService.CreateProduct(request.Product);
        }
    }
}
