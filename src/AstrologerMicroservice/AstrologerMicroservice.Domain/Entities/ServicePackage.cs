using System.ComponentModel.DataAnnotations;

namespace AstrologerMicroservice.Domain.Entities
{
    public class ServicePackage
    {
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        public bool IsActive { get; set; } = true;

        public Astrologer Astrologer { get; set; } = null!;

        public ICollection<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();
    }
}