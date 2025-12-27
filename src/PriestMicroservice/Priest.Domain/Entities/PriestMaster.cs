using Priest.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public partial class PriestMaster : CatalogMetadata
    {
        public virtual ICollection<PriestExpertise> VariantMasters { get; set; } = new List<PriestExpertise>();
        public virtual ICollection<PriestLanguage> PriestLanguages { get; set; } = new List<PriestLanguage>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
        public virtual ICollection<PriestMedia> Medias { get; set; } = new List<PriestMedia>();
    }

}