using Product.Application.Contracts;

namespace Product.Application.Services
{
    public interface ISeedCatalogService
    {
        Task<bool> SeedCatalogAsync(SeedCatalogDto seedCatalogDto);
    }
}