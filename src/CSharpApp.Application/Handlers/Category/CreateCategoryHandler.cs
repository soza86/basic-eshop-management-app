using CSharpApp.Application.Commands.Category;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Core.Dtos.Category>
    {
        private readonly ICategoriesService _categoriesService;

        public CreateCategoryHandler(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<Core.Dtos.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoriesService.CreateCategory(request.Category);
        }
    }
}
