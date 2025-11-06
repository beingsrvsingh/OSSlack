using Shared.Domain.Entities;
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

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MRP { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }
        public int AvailableSlots { get; set; } = 1;

        public bool IsDefault { get; set; } = false;

        // Attributes associated with this expertise
        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public virtual ICollection<TempleExpertiseImage> TempleExpertiseImages { get; set; } = new List<TempleExpertiseImage>();
        public virtual ICollection<TempleAddon> TempleAddons { get; set; } = new List<TempleAddon>();
    }

}
