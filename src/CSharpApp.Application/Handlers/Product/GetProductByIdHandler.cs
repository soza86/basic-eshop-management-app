using CSharpApp.Application.Queries.Product;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Core.Dtos.Product?>
    {
        private readonly IProductsService _productsService;
        private readonly IMapper<ProductServiceModel, Core.Dtos.Product> _customProductMapper;

        public GetProductByIdHandler(IProductsService productsService, IMapper<ProductServiceModel, Core.Dtos.Product> customProductMapper)
        {
            _productsService = productsService;
            _customProductMapper = customProductMapper;
        }

        public async Task<Core.Dtos.Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productsService.GetProductById(request.Id);
            return _customProductMapper.Map(result);
        }
    }
}
