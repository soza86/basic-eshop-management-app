using CSharpApp.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CSharpApp.Infrastructure.ExternalServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly RestApiSettings _options;

        public JwtTokenService(HttpClient httpClient, 
                               IMemoryCache cache, 
                               IOptions<RestApiSettings> options)
        {
            _httpClient = httpClient;
            _cache = cache;
            _options = options.Value;
        }

        public async Task<string> GetToken()
        {
            if (_cache.TryGetValue("jwt_token", out string token))
                return token;
            
            var request = new
            {
                email = _options.Username,
                password = _options.Password,
            };

            var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}{_options.Auth}", request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to retrieve JWT token");
            
            var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
            token = result?.AccessToken ?? throw new Exception("Invalid JWT response");

            _cache.Set("jwt_token", token, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(20)
            });

            return token;
        }
    }
}