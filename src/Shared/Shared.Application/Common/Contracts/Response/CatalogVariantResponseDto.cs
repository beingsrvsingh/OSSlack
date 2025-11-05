using Shared.Domain.Entities;

namespace Shared.Application.Common.Contracts.Response
{
    public class CatalogVariantResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? MRP { get; set; }
        public int? StockQuantity { get; set; }
        public int? DurationMinutes { get; set; }
        public int? AvailableSlots { get; set; }
        public BookingType? BookingType { get; set; }

        public List<MediaResponseDto> Media { get; set; } = new List<MediaResponseDto>();
        public List<AddonResponseDto> Addons { get; set; } = new List<AddonResponseDto>();
        public List<AttributeResponseDto> Attributes { get; set; } = new List<AttributeResponseDto>();
    }
}
