using Mapster;
using Product.Application.Contracts;
using Product.Application.Services;
using Product.Domain.Core.UOW;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Infrastructure.Services
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
                foreach (var cat in seedCatalogDto.ProductMasters)
                {
                    var exists = await unitOfWork.ProductRepository.AnyAsync(c => c.Id == cat.Id);
                    if (!exists)
                    {
                        var category = cat.Adapt<ProductMaster>();
                        await unitOfWork.ProductRepository.AddAsync(category);
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