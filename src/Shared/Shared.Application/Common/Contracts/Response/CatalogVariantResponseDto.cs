using Shared.Domain.Entities;

namespace Shared.Application.Common.Contracts.Response
{
    using System.Text.Json.Serialization;

    public class CatalogVariantResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("price")]
        public PriceResponseDto Price { get; set; } = null!;

        [JsonPropertyName("stock_quantity")]
        public int? StockQuantity { get; set; }

        [JsonPropertyName("duration_minutes")]
        public int? DurationMinutes { get; set; } = 0;

        [JsonPropertyName("available_slots")]
        public int? AvailableSlots { get; set; } = 0;

        [JsonPropertyName("booking_type")]
        public BookingType? BookingType { get; set; } = Domain.Entities.BookingType.Online;

        [JsonPropertyName("media")]
        public List<MediaResponseDto> Media { get; set; } = new List<MediaResponseDto>();

        [JsonPropertyName("addons")]
        public List<AddonResponseDto> Addons { get; set; } = new List<AddonResponseDto>();

        [JsonPropertyName("attributes")]
        public List<AttributeResponseDto> Attributes { get; set; } = new List<AttributeResponseDto>();
    }

}
