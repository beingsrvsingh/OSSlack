using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CategoryAttributeGroupRepository : Repository<CatalogAttributeGroupMaster>, ICatalogAttributeGroupRepository
    {
        private readonly CatalogDbContext dbContext;

        public CategoryAttributeGroupRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        // Methods for CatalogAttributeGroupMaster

        public async Task<CatalogAttributeGroupMaster?> GetGroupByIdAsync(int id)
        {
            return await dbContext.CatalogAttributeGroupMasters
                .FirstOrDefaultAsync(g => g.Id == id && g.IsActive);
        }

        public async Task<IEnumerable<CatalogAttributeGroupMaster>> GetAllGroupsAsync()
        {
            return await dbContext.CatalogAttributeGroupMasters
                .Where(g => g.IsActive)
                .OrderBy(g => g.SortOrder)
                .ToListAsync();
        }

        public async Task AddGroupAsync(CatalogAttributeGroupMaster group)
        {
            await dbContext.CatalogAttributeGroupMasters.AddAsync(group);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateGroupAsync(CatalogAttributeGroupMaster group)
        {
            dbContext.CatalogAttributeGroupMasters.Update(group);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int id)
        {
            var group = await GetGroupByIdAsync(id);
            if (group != null)
            {
                group.IsActive = false;  // soft delete
                await UpdateGroupAsync(group);
            }
        }

        // Methods for CategoryAttributeGroupMapping

        public async Task<IEnumerable<CategoryAttributeGroupMapping>> GetMappingsByCategoryIdAsync(int categoryId)
        {
            return await dbContext.CategoryAttributeGroupMappings
                .Where(m => m.CategoryMasterId == categoryId)
                .Include(m => m.AttributeGroup)
                .OrderBy(m => m.SortOrder)
                .ToListAsync();
        }

        public async Task AddMappingAsync(CategoryAttributeGroupMapping mapping)
        {
            await dbContext.CategoryAttributeGroupMappings.AddAsync(mapping);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveMappingAsync(int id)
        {
            var mapping = await dbContext.CategoryAttributeGroupMappings.FindAsync(id);
            if (mapping != null)
            {
                dbContext.CategoryAttributeGroupMappings.Remove(mapping);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}