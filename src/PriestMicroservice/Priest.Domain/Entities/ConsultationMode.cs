using Priest.Domain.Entities.Enums;
using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriestMicroservice.Domain.Entities
{
    public class ConsultationMode
    {
        [Key]
        public int Id { get; set; }

        public BasePrice Price { get; set; } = new BasePrice();
        public int StockQuantity { get; set; } = 1;
        public bool IsDefault { get; set; } = false;

        public int ExpertiseId { get; set; }

        public int ConsultationModeMasterId { get; set; }        

        [ForeignKey(nameof(ExpertiseId))]
        public PriestExpertise Expertise { get; set; } = null!;

        [ForeignKey(nameof(ConsultationModeMasterId))]
        public virtual ConsultationModeMaster ConsultationModeMaster { get; set; } = null!;
    }
}