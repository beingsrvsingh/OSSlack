using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class ServicePackage
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        public int SubCategoryId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsActive { get; set; } = true;

        // Snapshots (optional, for UI performance)
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }

        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        public Astrologer Astrologer { get; set; } = null!;

        public ICollection<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();
        public ICollection<ServiceTagPackage> ServiceTagPackages { get; set; } = new List<ServiceTagPackage>();

    }
}