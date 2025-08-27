using Catalog.Domain.Entities;
using Shared.Domain.Repository;

namespace Catalog.Domain.Core.Repository
{
    public interface ICatalogAttributeGroupRepository : IRepository<CatalogAttributeGroupMaster>
    {
        // CatalogAttributeGroupMaster methods
        Task<CatalogAttributeGroupMaster?> GetGroupByIdAsync(int id);
        Task<IEnumerable<CatalogAttributeGroupMaster>> GetAllGroupsAsync();
        Task AddGroupAsync(CatalogAttributeGroupMaster group);
        Task UpdateGroupAsync(CatalogAttributeGroupMaster group);
        Task DeleteGroupAsync(int id);

        // CategoryAttributeGroupMapping methods
        Task<IEnumerable<CategoryAttributeGroupMapping>> GetMappingsByCategoryIdAsync(int categoryId);
        Task AddMappingAsync(CategoryAttributeGroupMapping mapping);
        Task RemoveMappingAsync(int id);
    }


}