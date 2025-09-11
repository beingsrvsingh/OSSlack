using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TempleId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }

        // Basic Info
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool HasSchedule { get; set; } = false;

        // Pricing and Duration
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public TimeSpan Duration { get; set; }

        // Optional: If you want ratings (like in Priest)
        [Column(TypeName = "decimal(3,2)")]
        public decimal AverageRating { get; set; } = 0;

        public int TotalRatings { get; set; } = 0;

        // Flags
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster Temple { get; set; } = null!;

        // Optional: Attributes or Modes (if needed later)
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
    }

}
