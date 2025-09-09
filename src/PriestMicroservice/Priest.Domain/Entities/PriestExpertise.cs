using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class PriestExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PriestId { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        // Expertise Info
        public int YearsOfExperience { get; set; }
        public string? ProficiencyLevel { get; set; }

        // Package Info
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; } = true;

        // Snapshots
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster Priest { get; set; } = null!;

        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public ICollection<ConsultationMode> ConsultationModes { get; set; } = new List<ConsultationMode>();

    }

}