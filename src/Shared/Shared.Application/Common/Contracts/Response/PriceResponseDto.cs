using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    public partial class PriceResponseDto
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("mrp")]
        public decimal? Mrp { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = "INR";

        [JsonPropertyName("discount")]
        public decimal? Discount { get; set; } = 0;

        [JsonPropertyName("tax")]
        public decimal? Tax { get; set; } = 0;

        [JsonPropertyName("discounted_amount")]
        public decimal? DiscountedAmount => Mrp.HasValue ? Mrp - Amount : null;
    }
}
