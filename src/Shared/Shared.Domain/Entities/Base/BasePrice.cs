using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public partial class BasePrice
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Mrp { get; set; }
        public string Currency { get; set; } = "INR";
        public decimal? Discount { get; set; } = 0;
        public decimal? Tax { get; set; } = 0;
        public decimal? DiscountedAmount => Mrp.HasValue ? Mrp - Amount : null;
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}
