using CSharpApp.Api.Middlewares;
using CSharpApp.Application;
using CSharpApp.Application.Commands.Category;
using CSharpApp.Application.Commands.Product;
using CSharpApp.Application.Configuration;
using CSharpApp.Application.Queries.Category;
using CSharpApp.Application.Queries.Product;
using CSharpApp.Core.Dtos;
using MediatR;

namespace CSharpApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Logging.ClearProviders().AddSerilog(logger);

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDefaultConfiguration();
            builder.Services.AddHttpConfiguration();
            builder.Services.AddProblemDetails();
            builder.Services.AddApiVersioning();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly));
            builder.Services.AddMappingConfiguration();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            app.UseMiddleware<CustomLoggingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            var versionedEndpointRouteBuilder = app.NewVersionedApi();

            versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/products", async (IMediator mediator, CancellationToken cancellationToken) =>
                {
                    var products = await mediator.Send(new GetProductsQuery(), cancellationToken);
                    return products;
                })
                .WithName("GetProducts")
                .HasApiVersion(1.0);

            versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/categories", async (IMediator mediator, CancellationToken cancellationToken) =>
            {
                var categories = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
                return categories;
            })
                .WithName("GetCategories")
                .HasApiVersion(1.0);

            versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/products/{productId}", async (int productId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var product = await mediator.Send(new GetProductByIdQuery(productId), cancellationToken);
                return product;
            })
                .WithName("GetProductById")
                .HasApiVersion(1.0);

            versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/categories/{categoryId}", async (int categoryId, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var category = await mediator.Send(new GetCategoryByIdQuery(categoryId), cancellationToken);
                return category;
            })
                .WithName("GetCategoryById")
                .HasApiVersion(1.0);

            versionedEndpointRouteBuilder.MapPost("api/v{version:apiVersion}/products", async (CreateProduct product, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var newProduct = await mediator.Send(new CreateProductCommand(product), cancellationToken);
                return newProduct;
            })
                .WithName("CreateProduct")
                .HasApiVersion(1.0);

            versionedEndpointRouteBuilder.MapPost("api/v{version:apiVersion}/categories", async (CreateCategory category, IMediator mediator, CancellationToken cancellationToken) =>
            {
                var newCategory = await mediator.Send(new CreateCategoryCommand(category), cancellationToken);
                return newCategory;
            })
                .WithName("CreateCategory")
                .HasApiVersion(1.0);

            app.Run();
        }
    }
}