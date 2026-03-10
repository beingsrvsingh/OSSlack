using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;

namespace TempleMicroservice.Domain.Entities
{
    public class ConsultationMode
    {
        [Key]
        public int Id { get; set; }

        public int ExpertiseId { get; set; }
        [ForeignKey(nameof(ExpertiseId))]
        public virtual TempleExpertise Expertise { get; set; } = null!;

        public ConsultationModeType Mode { get; set; }

        public BasePrice Price { get; set; } = null!;
        public int? StockQuantity { get; set; }
        public bool IsDefault { get; set; } = false;

        public virtual ICollection<TempleAddon> Addons { get; set; } = new List<TempleAddon>();
        public virtual ICollection<AttributeValue> Attributes { get; set; } = new List<AttributeValue>();
        public virtual ICollection<TempleExpertiseMedia> Medias { get; set; } = new List<TempleExpertiseMedia>();
    }
}