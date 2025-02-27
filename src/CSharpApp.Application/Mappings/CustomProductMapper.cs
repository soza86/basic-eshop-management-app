using CSharpApp.Core.Models;

namespace CSharpApp.Application.Mappings
{
    public class CustomProductMapper : IMapper<ProductServiceModel, Product>, IMapper<Product, ProductServiceModel>, IMapper<CreateProduct, CreateProductServiceModel>
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
                CreationAt = source.CreationAt,
                UpdatedAt = source.UpdatedAt,
            };
        }

        public List<ProductServiceModel> Map(IReadOnlyCollection<Product> source)
        {
            throw new NotImplementedException();
        }

        public CreateProductServiceModel Map(CreateProduct source)
        {
            return new CreateProductServiceModel
            {
                Title = source.Title,
                Description = source.Description,
                Price = source.Price,
                Images = source.Images,
                CategoryId = source.CategoryId,
            };
        }

        public List<CreateProductServiceModel> Map(IReadOnlyCollection<CreateProduct> source)
        {
            throw new NotImplementedException();
        }
    }
}
