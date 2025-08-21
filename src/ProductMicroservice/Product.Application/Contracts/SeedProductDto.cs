using Product.Domain.Entities;

namespace Product.Application.Contracts
{
    public class SeedProductDto
    {
        public IList<ProductMaster> ProductMasters { get; set; } = new List<ProductMaster>();

    }
}