using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class SeedCatalogDto
    {
        public IList<ProductMaster> ProductMasters { get; set; } = new List<ProductMaster>();
    }
}