
using Shared.Application.Common.Contracts;
using Kathavachak.Application.Contracts;

namespace Kathavachak.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BaseCatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}