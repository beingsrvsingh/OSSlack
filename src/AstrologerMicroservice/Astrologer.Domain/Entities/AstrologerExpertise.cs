
using Astrologer.Domain.Entities;
using Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstrologerMicroservice.Domain.Entities
{
    public class AstrologerExpertise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int AstrologerId { get; set; }

        [ForeignKey(nameof(AstrologerId))]
        public virtual AstrologerMaster Astrologer { get; set; } = null!;

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

        public virtual ICollection<AstrologerAttributeValue> AstrologerAttributeValues { get; set; } = new List<AstrologerAttributeValue>();
        public ICollection<AstrologerConsultationMode> ConsultationModes { get; set; } = new List<AstrologerConsultationMode>();
        public virtual ICollection<AstrologerAddon> AstrologerAddons { get; set; } = new List<AstrologerAddon>();
        public virtual ICollection<AstrologerExpertiesMedia> AstrologerExpertiseMedia { get; set; } = new List<AstrologerExpertiesMedia>();

    }


}