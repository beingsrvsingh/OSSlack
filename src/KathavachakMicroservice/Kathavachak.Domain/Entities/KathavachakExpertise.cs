using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }        
        public int KathavachakId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        // Expertise Info
        public int YearsOfExperience { get; set; }
        public string? ProficiencyLevel { get; set; } // e.g., Beginner, Expert, etc.

        // Package Info
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsActive { get; set; } = true;

        // Catalog MS Snapshot        
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;

        public ICollection<KathavachakAttributeValue> KathavachakAttributeValues { get; set; } = new List<KathavachakAttributeValue>();
    }

}
