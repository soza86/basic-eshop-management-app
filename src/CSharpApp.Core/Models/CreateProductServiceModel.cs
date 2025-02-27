namespace CSharpApp.Core.Models
{
    public class CreateProductServiceModel
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public int? Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("images")]
        public List<string> Images { get; set; } = [];

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }
    }
}
