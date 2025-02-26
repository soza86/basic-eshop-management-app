using CSharpApp.Application.Queries.Category;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Core.Dtos.Category?>
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper<CategoryServiceModel, Core.Dtos.Category> _customCategoryMapper;

        public GetCategoryByIdHandler(ICategoriesService categoriesService, 
                                      IMapper<CategoryServiceModel, Core.Dtos.Category> customCategoryMapper)
        {
            _categoriesService = categoriesService;
            _customCategoryMapper = customCategoryMapper;
        }

        public async Task<Core.Dtos.Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _categoriesService.GetCategoryById(request.Id);
            return _customCategoryMapper.Map(result);
        }
    }
}
