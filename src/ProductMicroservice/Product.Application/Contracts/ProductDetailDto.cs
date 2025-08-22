
using System.Text.Json.Serialization;

namespace Product.Application.Contracts
{
    public class ProductDetailDto : ProductBySubCategoryResponseDto
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("includes")]
        public List<string>? Includes { get; set; } = new List<string>();

        [JsonPropertyName("preparationtime")]
        public string? PreparationTime { get; set; }
    }

}