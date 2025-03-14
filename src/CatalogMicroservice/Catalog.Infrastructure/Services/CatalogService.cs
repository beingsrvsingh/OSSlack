using Catalog.Application.Services;
using Catalog.Domain.Core.UOW;
using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IUnitOfWork unitOfWork;

        public CatalogService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CategoryMaster>> GetAllCatalog()
        {
            return await unitOfWork.CatalogRepository.GetAllAsync();
        }
    }
}
