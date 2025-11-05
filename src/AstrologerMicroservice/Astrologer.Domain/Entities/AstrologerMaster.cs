using Astrologer.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{    
    public partial class AstrologerMaster : CatalogMetadata
    {
        public virtual ICollection<AstrologerLanguage> AstrologerLanguages { get; set; } = new List<AstrologerLanguage>();
        public virtual ICollection<AstrologerExpertise> AstrologerExpertises { get; set; } = new List<AstrologerExpertise>();
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<AstrologerAttributeValue> AttributeValues { get; set; } = new List<AstrologerAttributeValue>();
        public virtual ICollection<AstrologerAddon> AstrologerAddons { get; set; } = new List<AstrologerAddon>();
        public virtual ICollection<AstrologerMedia> AstrologerMedia { get; set; } = new List<AstrologerMedia>();
    }

}