using CSharpApp.Application.Queries.Product;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IReadOnlyCollection<Core.Dtos.Product?>>
    {
        private readonly IProductsService _productsService;

        public GetProductsHandler(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public async Task<IReadOnlyCollection<Core.Dtos.Product?>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await _productsService.GetProducts();
        }
    }
}
