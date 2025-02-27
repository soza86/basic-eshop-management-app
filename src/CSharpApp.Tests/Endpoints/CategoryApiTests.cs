using CSharpApp.Application.Commands.Category;
using CSharpApp.Application.Queries.Category;
using CSharpApp.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace CSharpApp.Tests.Endpoints
{
    public class CategoryApiTests : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly HttpClient _client;

        public CategoryApiTests(WebApplicationFactory<Api.Program> factory)
        {

            _mediatorMock = new Mock<IMediator>();
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mediatorMock.Object);
                });
            }).CreateClient();
        }

        [Fact]
        public async Task Given_IRequestForAllCategories_When_GetCategories_Then_ReturnsCollectionOfCategories()
        {
            // Arrange
            var categoriesList = new List<Category>
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
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(categoriesList);

            // Act
            var response = await _client.GetAsync("api/v1/categories");

            // Assert
            var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, categories.Count);
            Assert.Equal(1, categories.ElementAtOrDefault(0)?.Id);
            Assert.Equal("Test Name", categories.ElementAtOrDefault(0)?.Name);
            Assert.Equal("Test Image", categories.ElementAtOrDefault(0)?.Image);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCategoriesQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Given_IRequestForSpecificCategoryByItsId_When_GetCategoryById_Then_ReturnsCategory()
        {
            // Arrange
            var id = 1;
            var category = new Category
            {
                Id = 1,
                Name = "Test Name",
                Image = "Test Image",
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);

            // Act
            var response = await _client.GetAsync($"api/v1/categories/{id}");
            var result = await response.Content.ReadFromJsonAsync<Category>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, result?.Id);
            Assert.Equal("Test Name", result?.Name);
            Assert.Equal("Test Image", result?.Image);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewCategory_When_CreateCategory_Then_ReturnsNewCategory()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Test Name",
                Image = "Test Image",
                CreationAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(category);

            // Act
            var response = await _client.PostAsJsonAsync($"api/v1/categories", category);
            var newCategory = await response.Content.ReadFromJsonAsync<Category>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, newCategory?.Id);
            Assert.Equal("Test Name", newCategory?.Name);
            Assert.Equal("Test Image", newCategory?.Image);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateCategoryCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
