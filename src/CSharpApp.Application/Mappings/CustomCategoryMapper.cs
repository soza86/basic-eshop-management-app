using CSharpApp.Core.Models;

namespace CSharpApp.Application.Mappings
{
    public class CustomCategoryMapper : IMapper<CategoryServiceModel, Category>, IMapper<Category, CategoryServiceModel>, IMapper<CreateCategory, CreateCategoryServiceModel>
    {
        public Category Map(CategoryServiceModel source)
        {
            return new Category
            {
                Id = source.Id,
                Name = source.Name,
                Image = source.Image,
                CreationAt = source.CreationAt,
                UpdatedAt = source.UpdatedAt,
            };
        }

        public List<Category> Map(IReadOnlyCollection<CategoryServiceModel> source)
        {
            var categories = new List<Category>();
            foreach (var item in source)
            {
                categories.Add(new Category
                {
                    Id = item.Id,
                    Name = item.Name,
                    Image = item.Image,
                    CreationAt = item.CreationAt,
                    UpdatedAt = item.UpdatedAt,
                });
            }
            return categories;
        }

        public CategoryServiceModel Map(Category source)
        {
            return new CategoryServiceModel
            {
                Id = source.Id,
                Name = source.Name,
                Image = source.Image,
                CreationAt = source.CreationAt,
                UpdatedAt = source.UpdatedAt,
            };
        }

        public List<CategoryServiceModel> Map(IReadOnlyCollection<Category> source)
        {
            throw new NotImplementedException();
        }

        public CreateCategoryServiceModel Map(CreateCategory source)
        {
            return new CreateCategoryServiceModel
            {
                Name = source.Name,
                Image = source.Image
            };
        }

        public List<CreateCategoryServiceModel> Map(IReadOnlyCollection<CreateCategory> source)
        {
            throw new NotImplementedException();
        }
    }
}
