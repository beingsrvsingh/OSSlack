
using Shared.Application.Common.Contracts;
using Temple.Application.Contracts;

namespace Temple.Application.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<BaseCatalogAttributeDto>> GetAttributesByCategoryId(int categoryId, int subCategoryId, bool isSummary = false);
    }

}