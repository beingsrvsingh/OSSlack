
using Shared.Application.Common.Contracts;

namespace Priest.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BaseCatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}