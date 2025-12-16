using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts
{
    public partial class BaseProductResponseDto
    {
        [JsonPropertyName("pid")]
        public string Pid { get; set; } = null!;

        [JsonPropertyName("type")]
        public string Type { get; set; } = "product";

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; } = 0;

        [JsonPropertyName("reviews")]
        public int Reviews { get; set; } = 0;

        [JsonPropertyName("categoryId")]
        public string CategoryId { get; set; } = string.Empty;

        [JsonPropertyName("subCategoryId")]
        public string SubCategoryId { get; set; } = string.Empty;

        [JsonPropertyName("categoryNameSnapshot")]
        public string? CategoryNameSnapshot { get; set; }

        [JsonPropertyName("subCategoryNameSnapshot")]
        public string? SubCategoryNameSnapshot { get; set; }

        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; } = true;

        [JsonPropertyName("isTrending")]
        public bool IsTrending { get; set; } = false;

        [JsonPropertyName("isFeatured")]
        public bool IsFeatured { get; set; } = false;

        [JsonPropertyName("tags")]
        public List<string>? Tags { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; } = "INR";

        [JsonPropertyName("price")]
        public double Price { get; set; } = 0;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;

        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 1;

    }


}
