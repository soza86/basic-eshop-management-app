using CSharpApp.Application.Queries.Category;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Core.Dtos.Category?>
    {
        private readonly ICategoriesService _categoriesService;

        public GetCategoryByIdHandler(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<Core.Dtos.Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesService.GetCategoryById(request.Id);
        }
    }
}
