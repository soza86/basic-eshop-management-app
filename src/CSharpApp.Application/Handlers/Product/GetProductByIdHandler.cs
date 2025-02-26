using CSharpApp.Application.Queries.Product;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Core.Dtos.Product?>
    {
        private readonly IProductsService _productsService;

        public GetProductByIdHandler(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<Core.Dtos.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _productsService.GetProductById(request.Id);
        }
    }
}
