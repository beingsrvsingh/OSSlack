using SearchAggregator.Domain.Entities;
using Shared.Domain.Repository;

namespace SearchAggregator.Domain.Core.Repository
{
    public interface IUserSearchHistoryRepository : IRepository<UserSearchHistory>
    {
        Task<UserSearchHistory> AddAsync(UserSearchHistory entity);

        Task<UserSearchHistory?> GetByIdAsync(int id);

        Task<List<UserSearchHistory>> GetByUserIdAsync(string userId);

        Task<List<UserSearchHistory>> GetAllAsync();

        Task<bool> DeleteAsync(int id);

        Task<bool> SaveChangesAsync();
        Task<List<UserSearchHistory>> GetTopGlobalSearchesAsync(int topN = 5);
    }

}
