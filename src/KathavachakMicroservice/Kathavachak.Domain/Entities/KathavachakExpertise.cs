using Shared.Domain.Entities;
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

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MRP { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }

        public bool IsDefault { get; set; } = false;

        public ICollection<KathavachakAttributeValue> KathavachakAttributeValues { get; set; } = new List<KathavachakAttributeValue>();

        public virtual ICollection<KathavachakAddon> KathavachakAddons { get; set; } = new List<KathavachakAddon>();
        public virtual ICollection<KathavachakExpertiseMedia> KathavachakExpertiseMedia { get; set; } = new List<KathavachakExpertiseMedia>();
    }

}
