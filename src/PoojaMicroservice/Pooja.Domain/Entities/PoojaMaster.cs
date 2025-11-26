using Shared.Domain.Entities.Base;

namespace Pooja.Domain.Entities
{
    public class PoojaMaster : CatalogMetadata
    {
        public virtual ICollection<PoojaVariantMaster> PoojaVariantMasters { get; set; } = new List<PoojaVariantMaster>();
        public virtual ICollection<PoojaAttributeValue> PoojaAttribute { get; set; } = new List<PoojaAttributeValue>();
        public virtual ICollection<PoojaImage> PoojaImages { get; set; } = new List<PoojaImage>();
        public virtual ICollection<PoojaAddon> PoojaAddons { get; set; } = new List<PoojaAddon>();
    }

}
