
using Catalog.Application.Contracts;

namespace Catalog.Application.Services
{
    public interface ISeedCatalogService
    {
        Task<bool> SeedCatalogAsync(SeedCatalogDto seedCatalogDto);
    }
}