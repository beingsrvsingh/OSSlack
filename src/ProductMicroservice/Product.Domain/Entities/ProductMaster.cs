using System.ComponentModel.DataAnnotations;

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
        public string? ImageUrl { get; set; }
        [MaxLength(50)]
        public string? SKU { get; set; }
        [MaxLength(50)]
        public string? ProductType { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsFeatured { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int SubCategoryId { get; set; }
        [MaxLength(100)]
        public string? SubCategoryNameSnapshot { get; set; }
        [MaxLength(100)]
        public string? CategoryNameSnapshot { get; set; }

        public virtual ICollection<ProductRegionPriceMaster> RegionPriceMaster { get; set; } = new List<ProductRegionPriceMaster>();
        public virtual ICollection<ProductVariantMaster> VariantMasters { get; set; } = new List<ProductVariantMaster>();
        public virtual ICollection<ProductAttributeMaster> ProductAttributeMasters { get; set; } = new List<ProductAttributeMaster>();
        public virtual ICollection<LocalizedProductInfoMaster> LocalizationMasters { get; set; } = new List<LocalizedProductInfoMaster>();
        public virtual ICollection<ProductTagMaster> ProductTagMasters { get; set; } = new List<ProductTagMaster>();
        public virtual ProductSEOInfoMaster? SEOInfoMaster { get; set; }
    }
}