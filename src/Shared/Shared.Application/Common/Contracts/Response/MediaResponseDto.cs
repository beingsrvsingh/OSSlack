using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public class MediaResponseDto
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = null!;

        [JsonPropertyName("url")]
        public string Url { get; set; } = null!;

        [JsonPropertyName("alt_text")]
        public string? AltText { get; set; }

        [JsonPropertyName("sort_order")]
        public int SortOrder { get; set; }
    }
}
