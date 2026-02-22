using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakMaster : CatalogMetadata
    {
        // Navigation        
        public ICollection<KathavachakExpertise> VariantMasters { get; set; } = new List<KathavachakExpertise>();
        public virtual ICollection<KathavachakAttributeValue> Attributes { get; set; } = new List<KathavachakAttributeValue>();
        public virtual ICollection<KathavachakAddon> Addons { get; set; } = new List<KathavachakAddon>();
        public ICollection<KathavachakLanguage> Languages { get; set; } = new List<KathavachakLanguage>();
        public ICollection<KathavachakTopic> Topics { get; set; } = new List<KathavachakTopic>();
        public ICollection<KathavachakSessionMode> SessionModes { get; set; } = new List<KathavachakSessionMode>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();        
        public ICollection<KathavachakMedia> Medias { get; set; } = new List<KathavachakMedia>();
        public virtual ICollection<ScheduleException> ScheduleExceptions { get; set; } = new List<ScheduleException>();
    }

}
