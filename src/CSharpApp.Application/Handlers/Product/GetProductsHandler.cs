using CSharpApp.Application.Queries.Product;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, List<Core.Dtos.Product>>
    {
        private readonly IProductsService _productsService;
        private readonly IMapper<ProductServiceModel, Core.Dtos.Product> _customProductMapper;

        public GetProductsHandler(IProductsService productsService,
                                  IMapper<ProductServiceModel, Core.Dtos.Product> customProductMapper)
        {
            _productsService = productsService;
            _customProductMapper = customProductMapper;
        }

        public async Task<List<Core.Dtos.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var result = await _productsService.GetProducts(cancellationToken);
            return _customProductMapper.Map(result);
        }
    }
}
