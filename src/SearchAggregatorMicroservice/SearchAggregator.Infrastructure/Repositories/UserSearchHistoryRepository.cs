using Microsoft.EntityFrameworkCore;
using SearchAggregator.Domain.Core.Repository;
using SearchAggregator.Domain.Entities;
using SearchAggregator.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;

namespace SearchAggregator.Infrastructure.Repositories
{
    public class UserSearchHistoryRepository : Repository<UserSearchHistory>, IUserSearchHistoryRepository
    {
        private readonly SearchDbContext _context;

        public UserSearchHistoryRepository(SearchDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<UserSearchHistory> AddAsync(UserSearchHistory entity)
        {
            await _context.UserSearchHistories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserSearchHistory?> GetByIdAsync(int id)
        {
            return await _context.UserSearchHistories.FindAsync(id);
        }

        public async Task<List<UserSearchHistory>> GetByUserIdAsync(string userId)
        {
            return await _context.UserSearchHistories
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.SearchedAt)
                .ToListAsync();
        }

        public async Task<List<UserSearchHistory>> GetAllAsync()
        {
            return await _context.UserSearchHistories
                .OrderByDescending(e => e.SearchedAt)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
            }

            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserSearchHistory>> GetTopGlobalSearchesAsync(int topN = 5)
        {
            return await _context.UserSearchHistories
                .OrderByDescending(u => u.SearchedAt)
                .Take(topN)
                .ToListAsync();
        }

    }

}
