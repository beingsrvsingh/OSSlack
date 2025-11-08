using Priest.Domain.Entities;
using Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class PriestExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PriestId { get; set; }

        [ForeignKey(nameof(PriestId))]
        public virtual PriestMaster PriestMaster { get; set; } = null!;

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

        public virtual ICollection<AttributeValue> AttributeValues { get; set; } = new List<AttributeValue>();
        public ICollection<ConsultationMode> ConsultationModes { get; set; } = new List<ConsultationMode>();
        public virtual ICollection<Addon> Addons { get; set; } = new List<Addon>();
        public virtual ICollection<PriestExpertiseMedia> PriestExpertiseMedia { get; set; } = new List<PriestExpertiseMedia>();

    }

}