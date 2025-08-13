using Catalog.Domain.Entities;

namespace Catalog.Application.Contracts
{
    public class SeedCatalogDto
    {
        public IList<CategoryMaster> CategoryMasters { get; set; } = new List<CategoryMaster>();
        public IList<SubCategoryMaster> SubCategoryMasters { get; set; } = new List<SubCategoryMaster>();
        public IList<PoojaKitItemMaster> PoojaKitItems { get; set;} = new List<PoojaKitItemMaster>();
    }
}