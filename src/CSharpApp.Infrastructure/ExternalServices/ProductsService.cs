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

    public async Task<IReadOnlyCollection<ProductServiceModel>> GetProducts(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(_restApiSettings.Products, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var res = JsonSerializer.Deserialize<List<ProductServiceModel>>(content);
        return res.AsReadOnly();
    }

    public async Task<ProductServiceModel> GetProductById(int productId, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"{_restApiSettings.Products}/{productId}", cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductServiceModel>(content);
    }

    public async Task<ProductServiceModel> CreateProduct(CreateProductServiceModel product, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"{_restApiSettings.Products}", product, cancellationToken);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProductServiceModel>(content);
    }
}