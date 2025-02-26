using CSharpApp.Core.Models;

namespace CSharpApp.Application.Mappings
{
    public class CustomProductMapper : IMapper<ProductServiceModel, Product>, IMapper<Product, ProductServiceModel>
    {
        public Product Map(ProductServiceModel source)
        {
            return new Product
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                Price = source.Price,
                Images = source.Images,
                Category = source.Category,
                CategoryId = source.CategoryId,
                CreationAt = source.CreationAt,
                UpdatedAt = source.UpdatedAt,
            };
        }

        public List<Product> Map(IReadOnlyCollection<ProductServiceModel> source)
        {
            var products = new List<Product>();
            foreach (var item in source)
            {
                products.Add(new Product
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    Price = item.Price,
                    Images = item.Images,
                    Category = item.Category,
                    CategoryId = item.CategoryId,
                    CreationAt = item.CreationAt,
                    UpdatedAt = item.UpdatedAt,
                });
            }
            return products;
        }

        public ProductServiceModel Map(Product source)
        {
            return new ProductServiceModel
            {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                Price = source.Price,
                Images = source.Images,
                Category = source.Category,
                CategoryId = source.CategoryId,
                CreationAt = source.CreationAt,
                UpdatedAt = source.UpdatedAt,
            };
        }

        public List<ProductServiceModel> Map(IReadOnlyCollection<Product> source)
        {
            throw new NotImplementedException();
        }
    }
}
