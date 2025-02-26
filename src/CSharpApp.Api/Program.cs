using CSharpApp.Api.Middlewares;
using CSharpApp.Application;
using CSharpApp.Application.Commands;
using CSharpApp.Application.Queries;
using CSharpApp.Core.Dtos;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

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

var app = builder.Build();

app.UseMiddleware<CustomLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

var versionedEndpointRouteBuilder = app.NewVersionedApi();

versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/products", async (IProductsService productsService) =>
    {
        var products = await productsService.GetProducts();
        return products;
    })
    .WithName("GetProducts")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/categories", async (IMediator mediator) =>
{
    var categories = await mediator.Send(new GetCategoriesQuery());
    return categories;
})
    .WithName("GetCategories")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/products/{productId}", async (int productId, IProductsService productsService) =>
{
    var product = await productsService.GetProductById(productId);
    return product;
})
    .WithName("GetProductById")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder.MapGet("api/v{version:apiVersion}/categories/{categoryId}", async (int categoryId, IMediator mediator) =>
{
    var category = await mediator.Send(new GetCategoryByIdQuery(categoryId));
    return category;
})
    .WithName("GetCategoryById")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder.MapPost("api/v{version:apiVersion}/products", async (Product product, IProductsService productsService) =>
{
    var newProduct = await productsService.CreateProduct(product);
    return newProduct;
})
    .WithName("CreateProduct")
    .HasApiVersion(1.0);

versionedEndpointRouteBuilder.MapPost("api/v{version:apiVersion}/categories", async (Category category, IMediator mediator) =>
{
    var newCategory = await mediator.Send(new CreateCategoryCommand(category));
    return newCategory;
})
    .WithName("CreateCategory")
    .HasApiVersion(1.0);

app.Run();