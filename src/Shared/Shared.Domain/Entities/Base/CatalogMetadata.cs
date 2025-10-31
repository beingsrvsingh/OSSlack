using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public partial class CatalogMetadata
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(300)]
        public string? ThumbnailUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public int Rating { get; set; }
        public int Reviews { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Optional, domain-dependent fields
        public string Currency { get; set; } = "INR";
        public bool? IsTrending { get; set; }        // any entity can be trending
        public bool? IsFeatured { get; set; }        // highlight entity
    }

}
