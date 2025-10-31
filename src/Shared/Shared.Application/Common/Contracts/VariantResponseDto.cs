using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts
{
    public class VariantResponseDto
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("mrp")]
        public decimal? MRP { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("isDefault")]
        public bool IsDefault { get; set; }

        [JsonPropertyName("attributes")]
        public Dictionary<string, string>? Attributes { get; set; }

        [JsonPropertyName("variant_images")]
        public List<string>? VariantImages { get; set; }

        // Optional future fields for temple / priest domains
        [JsonPropertyName("duration_minutes")]
        public int? DurationMinutes { get; set; }

        [JsonPropertyName("available_slots")]
        public int? AvailableSlots { get; set; }

        [JsonPropertyName("booking_type")]
        public string? BookingType { get; set; } // e.g., "Online", "In-person"        
    }
}
