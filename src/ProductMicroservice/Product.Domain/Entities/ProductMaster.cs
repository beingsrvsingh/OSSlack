using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductMaster
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        [MaxLength(300)]
        public string? ThumbnailUrl { get; set; }
        [MaxLength(50)]
        public string? SKU { get; set; }
        [MaxLength(50)]
        public string? ProductType { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsFeatured { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int CategoryId { get; set; }
        public int Rating { get; set; }
        public int Reviews { get; set; }
        public int SubCategoryId { get; set; }
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        public virtual ICollection<ProductRegionPriceMaster> RegionPriceMaster { get; set; } = new List<ProductRegionPriceMaster>();
        public virtual ICollection<ProductVariantMaster> VariantMasters { get; set; } = new List<ProductVariantMaster>();
        public virtual ICollection<LocalizedProductInfoMaster> LocalizationMasters { get; set; } = new List<LocalizedProductInfoMaster>();
        public virtual ICollection<ProductTagMaster> ProductTagMasters { get; set; } = new List<ProductTagMaster>();
        public virtual ProductSEOInfoMaster? SEOInfoMaster { get; set; }
        public virtual ICollection<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

    }
}