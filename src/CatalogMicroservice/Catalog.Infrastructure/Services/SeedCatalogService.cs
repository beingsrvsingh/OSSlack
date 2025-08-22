using Catalog.Application.Contracts;
using Catalog.Application.Services;
using Catalog.Domain.Core.UOW;
using Catalog.Domain.Entities;
using Mapster;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class SeedCatalogService : ISeedCatalogService
    {
        private readonly ILoggerService<SeedCatalogService> logger;
        private readonly IUnitOfWork unitOfWork;

        public SeedCatalogService(ILoggerService<SeedCatalogService> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> SeedCatalogAsync(SeedCatalogDto seedCatalogDto)
        {
            try
            {
                await unitOfWork.BeginTransactionAsync();

                // Seed Categories
                foreach (var cat in seedCatalogDto.CategoryMasters)
                {
                    var exists = await unitOfWork.CatalogRepository.AnyAsync(c => c.Id == cat.Id);
                    if (!exists)
                    {
                        var category = cat.Adapt<CategoryMaster>();
                        await unitOfWork.CatalogRepository.AddAsync(category);
                    }
                }
                await unitOfWork.SaveChangesAsync();

                // Seed Subcategories
                foreach (var subcat in seedCatalogDto.SubCategoryMasters)
                {
                    var exists = await unitOfWork.SubCategoryRepository.AnyAsync(s => s.Id == subcat.Id);
                    if (!exists)
                    {
                        var subCategory = subcat.Adapt<SubCategoryMaster>();
                        await unitOfWork.SubCategoryRepository.AddAsync(subCategory);
                    }
                }

                // Seed Pooja Kit Items
                foreach (var item in seedCatalogDto.PoojaKitItems)
                {
                    var exists = await unitOfWork.PoojaKitItemRepository.AnyAsync(s => s.Id == item.Id);
                    if (!exists)
                    {
                        var kitItem = item.Adapt<PoojaKitItemMaster>();
                        await unitOfWork.PoojaKitItemRepository.AddAsync(kitItem);
                    }
                }
                
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                logger.LogError(ex, "Error occurred during catalog seeding.");
                return false;
            }
        }

    }
}