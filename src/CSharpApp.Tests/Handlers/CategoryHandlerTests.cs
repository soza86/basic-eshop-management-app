using CSharpApp.Application.Commands.Category;
using CSharpApp.Application.Handlers.Category;
using CSharpApp.Application.Mappings;
using CSharpApp.Application.Queries.Category;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using CSharpApp.Core.Models;
using Moq;

namespace CSharpApp.Tests.Handlers
{
    public class CategoryHandlerTests
    {
        private readonly Mock<ICategoriesService> _categoryServiceMock;
        private readonly GetCategoriesHandler _getCategoriesHandler;
        private readonly GetCategoryByIdHandler _getCategoryByIdHandler;
        private readonly CreateCategoryHandler _createCategoryHandler;
        private readonly IMapper<CategoryServiceModel, Category> _customCategoryMapper;
        private readonly IMapper<CreateCategory, CreateCategoryServiceModel> _customCategoryServiceModelMapper;

        public CategoryHandlerTests()
        {
            _categoryServiceMock = new Mock<ICategoriesService>();
            _customCategoryMapper = new CustomCategoryMapper();
            _customCategoryServiceModelMapper = new CustomCategoryMapper();
            _getCategoriesHandler = new GetCategoriesHandler(_categoryServiceMock.Object, _customCategoryMapper);
            _getCategoryByIdHandler = new GetCategoryByIdHandler(_categoryServiceMock.Object, _customCategoryMapper);
            _createCategoryHandler = new CreateCategoryHandler(_categoryServiceMock.Object, _customCategoryServiceModelMapper, _customCategoryMapper);
        }

        [Fact]
        public async Task Given_IRequestForAllCategories_When_GetCategoriesHandler_Then_ReturnsCollectionOfCategories()
        {
            // Arrange
            var categoriesList = new List<CategoryServiceModel>
            {
                new() {
                    Id = 1,
                    Name = "Test Name",
                    Image = "Test Image",
                    CreationAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new() {
                    Id = 2,
                    Name = "Test Name 2",
                    Image = "Test Image 2",
                    CreationAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            };
            var query = new GetCategoriesQuery();
            var cancellationToken = new CancellationToken();
            _categoryServiceMock.Setup(a => a.GetCategories(It.IsAny<CancellationToken>())).ReturnsAsync(categoriesList);

            // Act
            var categories = await _getCategoriesHandler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(2, categories.Count);
            Assert.Equal(1, categories.ElementAtOrDefault(0)?.Id);
            Assert.Equal("Test Name", categories.ElementAtOrDefault(0)?.Name);
            Assert.Equal("Test Image", categories.ElementAtOrDefault(0)?.Image);
            _categoryServiceMock.Verify(repo => repo.GetCategories(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestForSpecificCategoryByItsId_When_GetCategoryByIdHandler_Then_ReturnsCategory()
        {
            // Arrange
            var id = 1;
            var categoryRecord = new CategoryServiceModel
            {
                Id = 1,
                Name = "Test Name",
                Image = "Test Image",
            };
            var query = new GetCategoryByIdQuery(id);
            var cancellationToken = new CancellationToken();
            _categoryServiceMock.Setup(a => a.GetCategoryById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(categoryRecord);

            // Act
            var category = await _getCategoryByIdHandler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(1, category?.Id);
            Assert.Equal("Test Name", category?.Name);
            Assert.Equal("Test Image", category?.Image);
            _categoryServiceMock.Verify(repo => repo.GetCategoryById(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewCategory_When_CreateCategoryHandler_Then_ReturnsNewCategory()
        {
            // Arrange
            var category = new CreateCategory
            {
                Name = "Test Name",
                Image = "Test Image"
            };
            var categoryRecord = new CategoryServiceModel
            {
                Id = 1,
                Name = "Test Name",
                Image = "Test Image",
                CreationAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            var command = new CreateCategoryCommand(category);
            var cancellationToken = new CancellationToken();
            _categoryServiceMock.Setup(a => a.CreateCategory(It.IsAny<CreateCategoryServiceModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(categoryRecord);

            // Act
            var newCategory = await _createCategoryHandler.Handle(command, cancellationToken);

            // Assert
            Assert.Equal(1, newCategory?.Id);
            Assert.Equal("Test Name", newCategory?.Name);
            Assert.Equal("Test Image", newCategory?.Image);
            _categoryServiceMock.Verify(repo => repo.CreateCategory(It.IsAny<CreateCategoryServiceModel>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
