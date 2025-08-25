using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Domain;

namespace CartMicroservice.Domain.Entities
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; } // ProductId, ServicePackageId, KitId

        public int? SubCategoryId { get; set; } // sub_category_id

        [Required]
        [MaxLength(20)]
        public ProviderType ProviderType { get; set; } // Product, Service, Kit

        [MaxLength(150)]
        public string ItemNameSnapshot { get; set; } = null!;

        [MaxLength(100)]
        public string? SkuSnapshot { get; set; }

        public decimal PriceSnapshot { get; set; }
        public decimal DiscountAmount { get; set; } = 0m;
        public decimal TaxAmount { get; set; } = 0m;

        public int Quantity { get; set; } = 1;

        public decimal PlatformFee { get; set; } = 0m;
        public decimal SurgeFee { get; set; } = 0m;

        [MaxLength(50)]
        public string? AppliedCouponCode { get; set; }

        public bool IsInStock { get; set; } = true;

        // For services like consultation, kundli, pooja
        public int? SelectedAstrologerId { get; set; }

        public DateTime? PreferredServiceDateTime { get; set; }

        [Column(TypeName = "jsonb")]
        public string? SelectedOptionsJson { get; set; }

        public bool IsGift { get; set; } = false;

        public decimal AdditionalFees { get; set; } = 0m;

        [MaxLength(250)]
        public string? GiftMessage { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false; // is_deleted

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // created_at

        public DateTime? UpdatedAt { get; set; } // updated_at

        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; } = null!;
    }

}