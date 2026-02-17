using System.Text.Json.Serialization;

namespace Cart.Application.Contracts
{
    public class BillItemDto
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "charge"; // charge, discount, etc.

        [JsonPropertyName("tooltip")]
        public string? Tooltip { get; set; }

        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
    }
}
