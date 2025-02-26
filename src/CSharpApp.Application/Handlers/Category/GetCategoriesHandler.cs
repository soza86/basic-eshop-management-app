using CSharpApp.Application.Queries.Category;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, IReadOnlyCollection<Core.Dtos.Category?>>
    {
        private readonly ICategoriesService _categoriesService;

        public GetCategoriesHandler(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IReadOnlyCollection<Core.Dtos.Category?>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoriesService.GetCategories();
        }
    }
}
