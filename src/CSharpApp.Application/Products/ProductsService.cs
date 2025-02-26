using CSharpApp.Core.Models;
using System.Net.Http.Json;

namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(HttpClient httpClient, IOptions<RestApiSettings> restApiSettings, 
        ILogger<ProductsService> logger)
    {
        _httpClient = httpClient;
        _restApiSettings = restApiSettings.Value;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ProductServiceModel>> GetProducts()
    {
        var response = await _httpClient.GetAsync(_restApiSettings.Products);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<List<ProductServiceModel>>(content);      
        return res.AsReadOnly();
    }

    public async Task<ProductServiceModel> GetProductById(int productId)
    {
        var response = await _httpClient.GetAsync($"{_restApiSettings.Products}/{productId}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductServiceModel>(content);
    }

    public async Task<ProductServiceModel> CreateProduct(ProductServiceModel product)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_restApiSettings.Products}", product);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductServiceModel>(content);
    }
}