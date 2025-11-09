namespace Shared.Application.Common.Contracts.Response
{
    using System.Text.Json.Serialization;

    public class CatalogResponseDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("reviews")]
        public int Reviews { get; set; }

        [JsonPropertyName("sub_category_id")]
        public required string SubCategoryId { get; set; }

        [JsonPropertyName("is_trending")]
        public bool? IsTrending { get; set; }

        [JsonPropertyName("is_featured")]
        public bool? IsFeatured { get; set; }

        [JsonPropertyName("price")]
        public PriceResponseDto? Price { get; set; }
        
        [JsonPropertyName("media")]
        public List<MediaResponseDto> Media { get; set; } = new List<MediaResponseDto>();

        [JsonPropertyName("variants")]
        public List<CatalogVariantResponseDto> Variants { get; set; } = new List<CatalogVariantResponseDto>();

        [JsonPropertyName("addons")]
        public List<AddonResponseDto> Addons { get; set; } = new List<AddonResponseDto>();

        [JsonPropertyName("attributes")]
        public List<AttributeResponseDto> Attributes { get; set; } = new List<AttributeResponseDto>();
    }

}
