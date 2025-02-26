using System.Text.Json.Serialization;

namespace CSharpApp.Infrastructure.Models
{
    public class JwtResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }
    }
}
