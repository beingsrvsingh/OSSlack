using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public class AddonResponseDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("price")]
        public PriceResponseDto Price { get; set; } = null!;
    }
}
