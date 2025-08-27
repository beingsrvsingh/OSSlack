using Catalog.Application.Services;
using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Catalog.Infrastructure.Services
{
    public class CatalogAttributeGroupService : ICatalogAttributeGroupService
    {
        private readonly ICatalogAttributeGroupRepository _groupRepository;
        private readonly ILoggerService<CatalogAttributeGroupService> _logger;

        public CatalogAttributeGroupService(
            ICatalogAttributeGroupRepository groupRepository,
            ILoggerService<CatalogAttributeGroupService> logger)
        {
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CatalogAttributeGroupMaster>> GetAllGroupsAsync()
        {
            return await _groupRepository.GetAllGroupsAsync();
        }

        public async Task<CatalogAttributeGroupMaster?> GetGroupByIdAsync(int id)
        {
            return await _groupRepository.GetGroupByIdAsync(id);
        }

        public async Task<bool> AddGroupAsync(CatalogAttributeGroupMaster group)
        {
            try
            {
                await _groupRepository.AddGroupAsync(group);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add attribute group");
                return false;
            }
        }

        public async Task<bool> UpdateGroupAsync(CatalogAttributeGroupMaster group)
        {
            try
            {
                await _groupRepository.UpdateGroupAsync(group);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update attribute group");
                return false;
            }
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            try
            {
                await _groupRepository.DeleteGroupAsync(id);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete attribute group");
                return false;
            }
        }

        public async Task<IEnumerable<CategoryAttributeGroupMapping>> GetMappingsByCategoryIdAsync(int categoryId)
        {
            return await _groupRepository.GetMappingsByCategoryIdAsync(categoryId);
        }

        public async Task<bool> AddMappingAsync(CategoryAttributeGroupMapping mapping)
        {
            try
            {
                await _groupRepository.AddMappingAsync(mapping);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add group mapping");
                return false;
            }
        }

        public async Task<bool> RemoveMappingAsync(int id)
        {
            try
            {
                await _groupRepository.RemoveMappingAsync(id);
                await _groupRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to remove group mapping");
                return false;
            }
        }
    }

}