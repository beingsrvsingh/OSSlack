using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakExpertise
    {
        [Key]
        public int Id { get; set; }
        
        public int KathavachakId { get; set; }
        public int CategoryId { get; set; } // Foreign key to CategoryMicroservice
        public int YearsOfExperience { get; set; }
        public string? ProficiencyLevel { get; set; } // e.g., Beginner, Expert, etc.
        public int SubCategoryId { get; set; }
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }
        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster Kathavachak { get; set; } = null!;
    }

}
