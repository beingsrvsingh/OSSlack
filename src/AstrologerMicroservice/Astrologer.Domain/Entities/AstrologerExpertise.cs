
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        // Expertise Info
        public int YearsOfExperience { get; set; }
        public string? ProficiencyLevel { get; set; }

        // Package Info
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; } = true;

        // Snapshots
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerEntity Astrologer { get; set; } = null!;

        public virtual ICollection<AstrologerAttributeValue> AstrologerAttributeValues { get; set; } = new List<AstrologerAttributeValue>();
        public ICollection<AstrologerConsultationMode> ConsultationModes { get; set; } = new List<AstrologerConsultationMode>();

    }


}