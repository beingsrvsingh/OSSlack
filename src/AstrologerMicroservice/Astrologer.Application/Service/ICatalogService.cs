
using Shared.Application.Common.Contracts;

namespace Astrologer.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BaseCatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}