using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICategoryLocalizedTextRepository : IRepository<CategoryLocalizedText>
    {
        Task<IEnumerable<CategoryLocalizedText>> GetByCategoryIdAsync(int categoryId);
    }
}