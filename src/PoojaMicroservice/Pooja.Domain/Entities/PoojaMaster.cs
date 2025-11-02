namespace Pooja.Domain.Entities
{
    public class PoojaMaster
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Pricing
        public decimal BasePrice { get; set; }
        public decimal? DiscountedPrice { get; set; } // Optional discounted price
        public bool IsPriceVariable { get; set; } // If price changes per temple or priest

        // Duration & Scheduling
        public TimeSpan Duration { get; set; }
        public bool IsAvailableOnline { get; set; } // e.g., for virtual poojas
        public bool IsTempleRequired { get; set; }
        public bool IsHomeAvailable { get; set; } // Pooja can be conducted at home
        public TimeSpan PreparationTime { get; set; } // For samagri setup

        // Priest Options
        public bool PriestIncluded { get; set; } // Temple provides priest
        public bool BringYourOwnPriestAllowed { get; set; }

        // Ratings & Reviews
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }

        // Media / Images
        public string? ImageUrl { get; set; }

        // Categories & Tags
        public int? CategoryId { get; set; }
        public List<string>? Tags { get; set; } // e.g., "Satyanarayana", "Griha Pravesh"

        // Relationships
        public ICollection<PoojaAddon> Addons { get; set; } = new List<PoojaAddon>();

        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }

}
