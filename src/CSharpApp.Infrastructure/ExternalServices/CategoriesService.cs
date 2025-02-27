using CSharpApp.Core.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace CSharpApp.Infrastructure.ExternalServices
{
    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient _httpClient;
        private readonly RestApiSettings _restApiSettings;

        public CategoriesService(HttpClient httpClient,
                                 IOptions<RestApiSettings> restApiSettings)
        {
            _httpClient = httpClient;
            _restApiSettings = restApiSettings.Value;
        }

        public async Task<IReadOnlyCollection<CategoryServiceModel>> GetCategories(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync(_restApiSettings.Categories, cancellationToken);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<List<CategoryServiceModel>>(content);
            return res.AsReadOnly();
        }

        public async Task<CategoryServiceModel> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{_restApiSettings.Categories}/{categoryId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryServiceModel>(content);
        }

        public async Task<CategoryServiceModel> CreateCategory(CreateCategoryServiceModel category, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_restApiSettings.Categories}", category, cancellationToken);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoryServiceModel>(content);
        }
    }
}
