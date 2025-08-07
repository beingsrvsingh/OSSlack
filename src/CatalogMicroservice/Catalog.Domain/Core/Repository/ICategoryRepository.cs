using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICategoryRepository : IRepository<CategoryMaster>
    {
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CategoryLocalizedText>> GetLocalizationsAsync(int categoryId);
        Task<CategoryLocalizedText> AddLocalizationAsync(CategoryLocalizedText localization);
    }
}
