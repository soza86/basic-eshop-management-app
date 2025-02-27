using CSharpApp.Core.Models;

namespace CSharpApp.Core.Interfaces
{
    public interface ICategoriesService
    {
        Task<IReadOnlyCollection<CategoryServiceModel>> GetCategories(CancellationToken cancellationToken);

        Task<CategoryServiceModel> GetCategoryById(int categoryId, CancellationToken cancellationToken);

        Task<CategoryServiceModel> CreateCategory(CreateCategoryServiceModel category, CancellationToken cancellationToken);
    }
}
