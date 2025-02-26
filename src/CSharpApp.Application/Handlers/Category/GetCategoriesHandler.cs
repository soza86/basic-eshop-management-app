using CSharpApp.Application.Queries.Category;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, List<Core.Dtos.Category>>
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper<CategoryServiceModel, Core.Dtos.Category> _customCategoryMapper;

        public GetCategoriesHandler(ICategoriesService categoriesService, 
                                    IMapper<CategoryServiceModel, Core.Dtos.Category> customCategoryMapper)
        {
            _categoriesService = categoriesService;
            _customCategoryMapper = customCategoryMapper;
        }

        public async Task<List<Core.Dtos.Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoriesService.GetCategories();
            return _customCategoryMapper.Map(result);
        }
    }
}
