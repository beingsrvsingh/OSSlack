namespace Shared.Application.Common.Contracts.Response
{
    public class CatalogResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ThumbnailUrl { get; set; }
        public bool IsActive { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

        public string Currency { get; set; } = "INR";
        public bool? IsTrending { get; set; }
        public bool? IsFeatured { get; set; }

        // Common media for UI
        public List<MediaResponseDto> Media { get; set; } = new List<MediaResponseDto>();

        // Variants for products/services
        public List<CatalogVariantResponseDto> Variants { get; set; } = new List<CatalogVariantResponseDto>();

        // Addons (product, service, pooja, etc.)
        public List<AddonResponseDto> Addons { get; set; } = new List<AddonResponseDto>();

        // Attributes (color, size, duration, slots, etc.)
        public List<AttributeResponseDto> Attributes { get; set; } = new List<AttributeResponseDto>();
    }
}
