using SearchAggregator.Domain.Entities;

namespace SearchAggregator.Application.Services
{
    public interface ISearchService
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
