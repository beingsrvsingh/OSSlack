using Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pooja.Domain.Entities
{
    public class PoojaVariantMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PoojaId { get; set; }

        [ForeignKey(nameof(PoojaId))]
        public virtual PoojaMaster PoojaMaster { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MRP { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }

        public bool IsDefault { get; set; } = false;

        public virtual ICollection<PoojaVariantImage> PoojaVariantImages { get; set; } = new List<PoojaVariantImage>();
        public virtual ICollection<PoojaAttributeValue> PoojaAttributeValues { get; set; } = new List<PoojaAttributeValue>();
        public virtual ICollection<PoojaAddon> PoojaAddons { get; set; } = new List<PoojaAddon>();
    }
}
