using Priest.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class PriestMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;
        public int Rating { get; set; }
        public int Reviews { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public virtual ICollection<PriestExpertise> PriestExpertises { get; set; } = new List<PriestExpertise>();
        public virtual ICollection<PriestLanguage> PriestLanguages { get; set; } = new List<PriestLanguage>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public virtual ICollection<PriestMedia> PriestMedias { get; set; } = new List<PriestMedia>();
        public virtual ICollection<ScheduleException> ScheduleExceptions { get; set; } = new List<ScheduleException>();
    }

}