using Priest.Domain.Entities;
using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class PriestExpertise
    {
        [Key]
        public int Id { get; set; }

        public int PriestId { get; set; }

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster PriestMaster { get; set; } = null!;

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }

        public int DurationMinutes { get; set; }
        public bool IsDefault { get; set; } = false;

        public virtual ICollection<AttributeValue> Attributes { get; set; } = new List<AttributeValue>();
        public virtual ICollection<ConsultationMode> ConsultationModes { get; set; } = new List<ConsultationMode>();
        public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
        public virtual ICollection<PriestExpertiseMedia> ExpertiseMedias { get; set; } = new List<PriestExpertiseMedia>();

    }

}