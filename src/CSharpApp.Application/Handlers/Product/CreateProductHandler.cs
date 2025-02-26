using CSharpApp.Application.Commands.Product;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Core.Dtos.Product>
    {
        private readonly IProductsService _productsService;
        private readonly IMapper<Core.Dtos.Product, ProductServiceModel> _customProductServiceModelMapper;
        private readonly IMapper<ProductServiceModel, Core.Dtos.Product> _customProductMapper;

        public CreateProductHandler(IProductsService productsService,
                                    IMapper<Core.Dtos.Product, ProductServiceModel> customProductServiceModelMapper,
                                    IMapper<ProductServiceModel, Core.Dtos.Product> customProductMapper)
        {
            _productsService = productsService;
            _customProductServiceModelMapper = customProductServiceModelMapper;
            _customProductMapper = customProductMapper;
        }

        public async Task<Core.Dtos.Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var requestModel = _customProductServiceModelMapper.Map(request.Product);
            var result = await _productsService.CreateProduct(requestModel);
            return _customProductMapper.Map(result);
        }
    }
}
