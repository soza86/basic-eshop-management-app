using CSharpApp.Application.Commands.Product;
using CSharpApp.Application.Queries.Product;
using CSharpApp.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace CSharpApp.Tests.Endpoints
{
    public class ProductApiTests : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly HttpClient _client;

        public ProductApiTests(WebApplicationFactory<Api.Program> factory)
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
        public async Task Given_IRequestForAllProducts_When_GetProducts_Then_ReturnsCollectionOfProducts()
        {
            // Arrange
            var productsList = new List<Product>
            {
                new() {
                    Id = 1,
                    Description = "Test description",
                    Price = 100,
                    Title = "Test title"
                },
                new() {
                    Id = 2,
                    Description = "Test description 2",
                    Price = 50,
                    Title = "Test title 2"
                }
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(productsList);

            // Act
            var response = await _client.GetAsync("api/v1/products");

            // Assert
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(2, products.Count);
            Assert.Equal(1, products.ElementAtOrDefault(0)?.Id);
            Assert.Equal("Test description", products.ElementAtOrDefault(0)?.Description);
            Assert.Equal(100, products.ElementAtOrDefault(0)?.Price);
            Assert.Equal("Test title", products.ElementAtOrDefault(0)?.Title);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetProductsQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Given_IRequestForSpecificProductByItsId_When_GetProductById_Then_ReturnsProduct()
        {
            // Arrange
            var id = 1;
            var productRecord = new Product
            {
                Id = 1,
                Description = "Test description",
                Price = 100,
                Title = "Test title"
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(productRecord);

            // Act
            var response = await _client.GetAsync($"api/v1/products/{id}");
            var product = await response.Content.ReadFromJsonAsync<Product>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, product?.Id);
            Assert.Equal("Test description", product?.Description);
            Assert.Equal(100, product?.Price);
            Assert.Equal("Test title", product?.Title);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewProduct_When_CreateProduct_Then_ReturnsNewProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Description = "Test description",
                Price = 100,
                Title = "Test title",
            };
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(product);

            // Act
            var response = await _client.PostAsJsonAsync($"api/v1/products", product);
            var newProduct = await response.Content.ReadFromJsonAsync<Product>();

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(1, newProduct?.Id);
            Assert.Equal("Test description", newProduct?.Description);
            Assert.Equal(100, newProduct?.Price);
            Assert.Equal("Test title", newProduct?.Title);
            _mediatorMock.Verify(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
