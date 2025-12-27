using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kathavachak.Domain.Entities
{
    public class KathavachakExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int KathavachakId { get; set; }

        [ForeignKey(nameof(KathavachakId))]
        public virtual KathavachakMaster KathavachakMaster { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public BasePrice Price { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }

        public bool IsDefault { get; set; } = false;

        public ICollection<KathavachakAttributeValue> Attributes { get; set; } = new List<KathavachakAttributeValue>();

        public virtual ICollection<KathavachakAddon> Addons { get; set; } = new List<KathavachakAddon>();
        public virtual ICollection<KathavachakExpertiseMedia> Medias { get; set; } = new List<KathavachakExpertiseMedia>();
    }

}
