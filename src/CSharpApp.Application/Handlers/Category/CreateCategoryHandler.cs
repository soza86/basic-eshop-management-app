﻿using CSharpApp.Application.Commands.Category;
using CSharpApp.Core.Models;
using MediatR;

namespace CSharpApp.Application.Handlers.Category
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Core.Dtos.Category>
    {
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper<CreateCategory, CreateCategoryServiceModel> _customCategoryServiceModelMapper;
        private readonly IMapper<CategoryServiceModel, Core.Dtos.Category> _customCategoryMapper;

        public CreateCategoryHandler(ICategoriesService categoriesService, 
                                     IMapper<CreateCategory, CreateCategoryServiceModel> customCategoryServiceModelMapper, 
                                     IMapper<CategoryServiceModel, Core.Dtos.Category> customCategoryMapper)
        {
            _categoriesService = categoriesService;
            _customCategoryServiceModelMapper = customCategoryServiceModelMapper;
            _customCategoryMapper = customCategoryMapper;
        }

        public async Task<Core.Dtos.Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var requestModel = _customCategoryServiceModelMapper.Map(request.Category);
            var result = await _categoriesService.CreateCategory(requestModel, cancellationToken);
            return _customCategoryMapper.Map(result);
        }
    }
}
