using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Core.Repository;
using CatalogUI.Domain.Entities;
using CatalogUI.Infrastructure.Persistence.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace CatalogUI.Infrastructure.Repositories
{
    public class LayoutRepository : Repository<Layout>, ILayoutRepository
    {
        private readonly CatalogUIDbContext _context;

        public LayoutRepository(CatalogUIDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<Layout?> GetByIdAsync(int id)
        {
            return await _context.Layouts.FindAsync(id);
        }

        public async Task<IEnumerable<Layout>> GetAllAsync()
        {
            return await _context.Layouts.ToListAsync();
        }

        public async Task<IEnumerable<Layout>> GetByPageAsync(string pageName)
        {
            return await _context.Layouts
                .Where(l => l.PageName == pageName && l.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Layout>> GetByTenantAndRoleAsync(string tenantId, string? userRole)
        {
            return await _context.Layouts
                .Where(l => l.TenantId == tenantId && l.IsActive && (userRole == null || l.UserRole == userRole))
                .ToListAsync();
        }

        public async Task AddAsync(Layout layout)
        {
            await _context.Layouts.AddAsync(layout);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Layout layout)
        {
            _context.Layouts.Update(layout);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var layout = await _context.Layouts.FindAsync(id);
            if (layout != null)
            {
                _context.Layouts.Remove(layout);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Layouts.AnyAsync(l => l.Id == id);
        }
    }
}