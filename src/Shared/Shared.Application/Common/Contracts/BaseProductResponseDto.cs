using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts
{
    public partial class BaseProductResponseDto
    {
        [JsonPropertyName("pid")]
        public string Pid { get; set; } = null!;

        [JsonPropertyName("cid")]
        public string Cid { get; set; } = string.Empty;

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("thumbnail_url")]
        public string? ThumbnailUrl { get; set; }

        [JsonPropertyName("cost")]
        public double Cost { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; } = 0;

        [JsonPropertyName("reviews")]
        public int Reviews { get; set; } = 0;

        [JsonPropertyName("categorytype")]
        public string? CategoryType { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;
        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 1;
    }
}
