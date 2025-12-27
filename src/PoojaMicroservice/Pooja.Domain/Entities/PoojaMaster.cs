using Shared.Domain.Entities.Base;

namespace Pooja.Domain.Entities
{
    public class PoojaMaster : CatalogMetadata
    {
        public virtual ICollection<PoojaVariantMaster> VariantMasters { get; set; } = new List<PoojaVariantMaster>();
        public virtual ICollection<PoojaAttributeValue> AttributeValues { get; set; } = new List<PoojaAttributeValue>();
        public virtual ICollection<PoojaImage> Medias { get; set; } = new List<PoojaImage>();
        public virtual ICollection<PoojaAddon> Addons { get; set; } = new List<PoojaAddon>();
    }

}
