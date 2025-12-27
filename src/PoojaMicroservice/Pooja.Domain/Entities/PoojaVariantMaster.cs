using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
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

        public BasePrice Price { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }

        public bool IsDefault { get; set; } = false;

        public virtual ICollection<PoojaVariantImage> Medias { get; set; } = new List<PoojaVariantImage>();
        public virtual ICollection<PoojaAttributeValue> Attributes { get; set; } = new List<PoojaAttributeValue>();
        public virtual ICollection<PoojaAddon> Addons { get; set; } = new List<PoojaAddon>();
    }
}
