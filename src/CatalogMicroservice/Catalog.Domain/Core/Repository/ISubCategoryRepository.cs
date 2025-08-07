using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ISubCategoryRepository : IRepository<SubCategoryMaster>
    {
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<SubCategoryLocalizedText>> GetLocalizationsAsync(int subCategoryId);
        Task<SubCategoryLocalizedText> AddLocalizationAsync(SubCategoryLocalizedText localization);
    }
}