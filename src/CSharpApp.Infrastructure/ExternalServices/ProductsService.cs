using CSharpApp.Core.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace CSharpApp.Infrastructure.ExternalServices;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;
    private readonly RestApiSettings _restApiSettings;

    public ProductsService(HttpClient httpClient, 
                           IOptions<RestApiSettings> restApiSettings)
    {
        _httpClient = httpClient;
        _restApiSettings = restApiSettings.Value;
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