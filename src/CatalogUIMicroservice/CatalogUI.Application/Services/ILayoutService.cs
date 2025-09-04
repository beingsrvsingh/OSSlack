using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Application.Contracts;
using CatalogUI.Domain.Entities;

namespace CatalogUI.Application.Services
{
    public interface ILayoutService
    {
        Task<LayoutDto?> GetLayoutByIdAsync(int id);
        Task<IEnumerable<LayoutDto>> GetAllLayoutsAsync();
        Task<IEnumerable<LayoutDto>> GetLayoutsByPageAsync(string pageName);
        Task<IEnumerable<LayoutDto>> GetLayoutsByTenantAndRoleAsync(string tenantId, string? userRole);

        Task<bool> AddLayoutAsync(CreateLayoutDto createDto);

        Task<bool> UpdateLayoutAsync(UpdateLayoutDto updateDto);

        Task<bool> DeleteLayoutAsync(int id);
    }

}