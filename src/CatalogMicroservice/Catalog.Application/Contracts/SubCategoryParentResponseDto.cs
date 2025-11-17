using System.Text.Json.Serialization;

namespace Catalog.Application.Contracts
{
    public class SubCategoryParentResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
        [JsonPropertyName("display_order")]
        public int DisplayOrder { get; set; }
    }
}
