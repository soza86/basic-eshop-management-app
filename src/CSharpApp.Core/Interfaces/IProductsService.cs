using CSharpApp.Core.Models;

namespace CSharpApp.Core.Interfaces;

public interface IProductsService
{
    Task<IReadOnlyCollection<ProductServiceModel>> GetProducts(CancellationToken cancellationToken);

    Task<ProductServiceModel> GetProductById(int productId, CancellationToken cancellationToken);

    Task<ProductServiceModel> CreateProduct(CreateProductServiceModel product, CancellationToken cancellationToken);
}