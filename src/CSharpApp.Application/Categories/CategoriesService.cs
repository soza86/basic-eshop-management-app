using System.Net.Http.Json;

namespace CSharpApp.Application.Categories
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

        public async Task<IReadOnlyCollection<Category>> GetCategories()
        {
            var response = await _httpClient.GetAsync(_restApiSettings.Categories);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<List<Category>>(content);
            return res.AsReadOnly();
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var response = await _httpClient.GetAsync($"{_restApiSettings.Categories}/{categoryId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(content);
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_restApiSettings.Categories}", category);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(content);
        }
    }
}
