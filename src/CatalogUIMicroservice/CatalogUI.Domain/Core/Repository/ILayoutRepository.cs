using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;

namespace CatalogUI.Domain.Core.Repository
{
    public interface ILayoutRepository
    {
        Task<Layout?> GetByIdAsync(int id);

        Task<IEnumerable<Layout>> GetAllAsync();

        Task<IEnumerable<Layout>> GetByPageAsync(string pageName);

        Task<IEnumerable<Layout>> GetByTenantAndRoleAsync(string tenantId, string? userRole);

        Task AddAsync(Layout layout);

        Task UpdateAsync(Layout layout);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);
    }
}