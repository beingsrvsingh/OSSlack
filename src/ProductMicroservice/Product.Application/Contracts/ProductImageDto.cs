using System.Text.Json.Serialization;

namespace Product.Application.Contracts
{
    public class ProductImageDto
    {
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; } = null!;
    }

}