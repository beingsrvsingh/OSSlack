using Shared.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    public class ProductMaster : CatalogMetadata
    {

        public virtual ICollection<ProductRegionPriceMaster> RegionPriceMaster { get; set; } = new List<ProductRegionPriceMaster>();
        public virtual ICollection<ProductVariantMaster> VariantMasters { get; set; } = new List<ProductVariantMaster>();
        public virtual ICollection<LocalizedProductInfoMaster> LocalizationMasters { get; set; } = new List<LocalizedProductInfoMaster>();
        public virtual ICollection<ProductTagMaster> ProductTagMasters { get; set; } = new List<ProductTagMaster>();
        public virtual ProductSEOInfoMaster? SEOInfoMaster { get; set; }
        public virtual ICollection<ProductAttributeValue> AttributeValues { get; set; } = new List<ProductAttributeValue>();        
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    }
}