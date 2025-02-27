namespace CSharpApp.Core.Models
{
    public class CreateCategoryServiceModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
