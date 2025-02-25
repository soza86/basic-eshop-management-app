using MediatR;

namespace CSharpApp.Application.Queries
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<Category?>>
    {
        private readonly ICategoriesService _categoriesService;

        public GetCategoriesHandler(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IReadOnlyCollection<Category?>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesService.GetCategories();
        }
    }
}
