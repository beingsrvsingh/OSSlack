using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ISubCategoryLocalizedTextRepository : IRepository<SubCategoryLocalizedText>
    {
        Task<IEnumerable<SubCategoryLocalizedText>> GetBySubCategoryIdAsync(int subCategoryId);

    }
}