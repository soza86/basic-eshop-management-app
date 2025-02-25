using MediatR;

namespace CSharpApp.Application.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
    {
        private readonly ICategoriesService _categoriesService;

        public GetCategoryByIdHandler(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesService.GetCategoryById(request.Id);
        }
    }
}
