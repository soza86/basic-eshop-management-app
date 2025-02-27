using CSharpApp.Core.Models;

namespace CSharpApp.Core.Interfaces;

public interface IProductsService
{
    Task<IReadOnlyCollection<ProductServiceModel>> GetProducts();

    Task<ProductServiceModel> GetProductById(int productId);

    Task<ProductServiceModel> CreateProduct(CreateProductServiceModel product);
}