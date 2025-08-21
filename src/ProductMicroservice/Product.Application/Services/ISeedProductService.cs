using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface ISeedProductService
    {
        Task<bool> SeedCatalogAsync(SeedProductDto seedCatalogDto);
    }
}