using System.Text.Json.Serialization;

namespace Cart.Application.Contracts
{
    public class CartItemDto
    {
        [JsonPropertyName("pid")]
        public string ProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("additional_fees")]
        public decimal AdditionalFees { get; set; } = 0m;

        [JsonPropertyName("provider_type")]
        public string ProviderType { get; set; } = "Product";
    }
}
