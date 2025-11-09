using System.Text.Json.Serialization;

namespace Product.Application.Contracts
{
    public class TrendingProductResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
