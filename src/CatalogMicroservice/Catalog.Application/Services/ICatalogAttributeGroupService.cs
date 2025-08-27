using Catalog.Domain.Entities;

namespace Catalog.Application.Services
{
    public interface ICatalogAttributeGroupService
    {
        Task<CatalogAttributeGroupMaster?> GetGroupByIdAsync(int id);
        Task<IEnumerable<CatalogAttributeGroupMaster>> GetAllGroupsAsync();
        Task<bool> AddGroupAsync(CatalogAttributeGroupMaster group);
        Task<bool> UpdateGroupAsync(CatalogAttributeGroupMaster group);
        Task<bool> DeleteGroupAsync(int id);

        Task<IEnumerable<CategoryAttributeGroupMapping>> GetMappingsByCategoryIdAsync(int categoryId);
        Task<bool> AddMappingAsync(CategoryAttributeGroupMapping mapping);
        Task<bool> RemoveMappingAsync(int id);
    }

}