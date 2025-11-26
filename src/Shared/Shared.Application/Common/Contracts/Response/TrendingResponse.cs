using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public class TrendingResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = "";

        [JsonPropertyName("scid")]
        public string Scid { get; set; } = "";

        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
    }
}
