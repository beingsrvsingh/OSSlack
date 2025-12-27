
using Astrologer.Domain.Entities;
using Shared.Domain.Entities;
using Shared.Domain.Entities.Base;
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

        public BasePrice Price { get; set; }

        public int? StockQuantity { get; set; }

        public int DurationMinutes { get; set; }
        public BookingType BookingType { get; set; }

        public bool IsDefault { get; set; } = false;        

        public virtual ICollection<AstrologerAttributeValue> Attributes { get; set; } = new List<AstrologerAttributeValue>();
        public ICollection<AstrologerConsultationMode> ConsultationModes { get; set; } = new List<AstrologerConsultationMode>();
        public virtual ICollection<AstrologerAddon> Addons { get; set; } = new List<AstrologerAddon>();
        public virtual ICollection<AstrologerExpertiesMedia> Media { get; set; } = new List<AstrologerExpertiesMedia>();

    }


}