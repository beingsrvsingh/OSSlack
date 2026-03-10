using System.Text.Json.Serialization;

namespace Shared.Application.Common.Contracts.Response
{
    // Base model for any hub (temple, store, etc.)
    public class HubDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string BannerUrl { get; set; } = string.Empty;

        public double Rating { get; set; } = 0;

        // e.g., "10 mins"
        public string? Distance { get; set; } = "1 KM";

        // "Open now", "Closed now", etc.
        public string? Status { get; set; }

        // e.g., "9:00 AM - 9:00 PM"
        public string? WorkingHours { get; set; }
        
        public List<CategoryDto> Categories { get; set; } = new();        
    }

    // Category model (generic for any service category)
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ItemDto> Items { get; set; } = new();
    }

    // Generic item model (services, products, rituals, etc.)
    public class ItemDto
    {
        public int Id { get; set; }
        public int ExpertiseId { get; set; }
        public int SubCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string ThumbnailUril { get; set; } = string.Empty;
        public bool HasAddon { get; set; }
        public bool HasModes { get; set; }
        public List<ConsultationModeDto> Modes { get; set; } = new();
        public List<AddonDto> AddOns { get; set; } = new();
        public string Availability { get; set; } = "available";
        public Dictionary<string, string>? Metadata { get; set; }
    }

    public class ConsultationModeDto
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsDefault { get; set; }
    }

    public class AddonDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("price")]
        public Decimal Price { get; set; } = new();
    }
}
