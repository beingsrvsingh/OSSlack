using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Temple.Domain.Entities
{
    public class TempleExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TempleId { get; set; }

        [ForeignKey(nameof(TempleId))]
        public virtual TempleMaster TempleMaster { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        
        public BasePrice Price { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }
        public int AvailableSlots { get; set; } = 1;

        public bool IsDefault { get; set; } = false;

        // Attributes associated with this expertise
        public virtual ICollection<AttributeValue> Attributes { get; set; } = new List<AttributeValue>();
        public virtual ICollection<TempleExpertiseImage> Media { get; set; } = new List<TempleExpertiseImage>();
        public virtual ICollection<TempleAddon> Addons { get; set; } = new List<TempleAddon>();
    }

}
