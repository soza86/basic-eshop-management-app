﻿using CSharpApp.Application.Commands.Product;
using CSharpApp.Application.Handlers.Product;
using CSharpApp.Application.Mappings;
using CSharpApp.Application.Queries.Product;
using CSharpApp.Core.Dtos;
using CSharpApp.Core.Interfaces;
using CSharpApp.Core.Models;
using Moq;

namespace CSharpApp.Tests.Handlers
{
    public class ProductHandlerTests
    {
        private readonly Mock<IProductsService> _productServiceMock;
        private readonly GetProductsHandler _getProductsHandler;
        private readonly GetProductByIdHandler _getProductByIdHandler;
        private readonly CreateProductHandler _createProductHandler;
        private readonly IMapper<ProductServiceModel, Product> _customProductMapper;
        private readonly IMapper<CreateProduct, CreateProductServiceModel> _customProductServiceModelMapper;

        public ProductHandlerTests()
        {
            _productServiceMock = new Mock<IProductsService>();
            _customProductMapper = new CustomProductMapper();
            _customProductServiceModelMapper = new CustomProductMapper();
            _getProductsHandler = new GetProductsHandler(_productServiceMock.Object, _customProductMapper);
            _getProductByIdHandler = new GetProductByIdHandler(_productServiceMock.Object, _customProductMapper);
            _createProductHandler = new CreateProductHandler(_productServiceMock.Object, _customProductServiceModelMapper, _customProductMapper);
        }

        [Fact]
        public async Task Given_IRequestForAllProducts_When_GetProductsHandler_Then_ReturnsCollectionOfProducts()
        {
            // Arrange
            var productsList = new List<ProductServiceModel>
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
            var query = new GetProductsQuery();
            var cancellationToken = new CancellationToken();
            _productServiceMock.Setup(a => a.GetProducts(It.IsAny<CancellationToken>())).ReturnsAsync(productsList);

            // Act
            var products = await _getProductsHandler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(2, products.Count);
            Assert.Equal(1, products.ElementAtOrDefault(0)?.Id);
            Assert.Equal("Test description", products.ElementAtOrDefault(0)?.Description);
            Assert.Equal(100, products.ElementAtOrDefault(0)?.Price);
            Assert.Equal("Test title", products.ElementAtOrDefault(0)?.Title);
            _productServiceMock.Verify(repo => repo.GetProducts(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestForSpecificProductByItsId_When_GetProductByIdHandler_Then_ReturnsProduct()
        {
            // Arrange
            var id = 1;
            var productRecord = new ProductServiceModel
            {
                Id = 1,
                Description = "Test description",
                Price = 100,
                Title = "Test title"
            };
            var query = new GetProductByIdQuery(id);
            var cancellationToken = new CancellationToken();
            _productServiceMock.Setup(a => a.GetProductById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(productRecord);

            // Act
            var product = await _getProductByIdHandler.Handle(query, cancellationToken);

            // Assert
            Assert.Equal(1, product?.Id);
            Assert.Equal("Test description", product?.Description);
            Assert.Equal(100, product?.Price);
            Assert.Equal("Test title", product?.Title);
            _productServiceMock.Verify(repo => repo.GetProductById(id, cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Given_IRequestToCreateNewProduct_When_CreateProductHandler_Then_ReturnsNewProduct()
        {
            // Arrange
            var product = new CreateProduct
            {
                CategoryId = 1,
                Description = "Test description",
                Price = 100,
                Title = "Test title",
            };
            var productRecord = new ProductServiceModel
            {
                Id = 1,
                Description = "Test description",
                Price = 100,
                Title = "Test title",
                Images = [],
                CreationAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            var command = new CreateProductCommand(product);
            var cancellationToken = new CancellationToken();
            _productServiceMock.Setup(a => a.CreateProduct(It.IsAny<CreateProductServiceModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(productRecord);

            // Act
            var newProduct = await _createProductHandler.Handle(command, cancellationToken);

            // Assert
            Assert.Equal(1, newProduct?.Id);
            Assert.Equal("Test description", newProduct?.Description);
            Assert.Equal(100, newProduct?.Price);
            Assert.Equal("Test title", newProduct?.Title);
            _productServiceMock.Verify(repo => repo.CreateProduct(It.IsAny<CreateProductServiceModel>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
