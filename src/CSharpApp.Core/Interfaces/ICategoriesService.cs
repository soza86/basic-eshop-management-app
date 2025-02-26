using CSharpApp.Core.Models;

namespace CSharpApp.Core.Interfaces
{
    public interface ICategoriesService
    {
        Task<IReadOnlyCollection<CategoryServiceModel>> GetCategories();

        Task<CategoryServiceModel> GetCategoryById(int categoryId);

        Task<CategoryServiceModel> CreateCategory(CategoryServiceModel category);
    }
}
