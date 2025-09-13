using Microsoft.Extensions.Logging;
using SearchAggregator.Application.Services;
using SearchAggregator.Domain.Core.Repository;
using SearchAggregator.Domain.Entities;

namespace SearchAggregator.Infrastructure.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> logger;
        private readonly IUserSearchHistoryRepository repository;

        public async Task<UserSearchHistory> AddAsync(UserSearchHistory entity)
        {
            try
            {
                var result = await repository.AddAsync(entity);
                await repository.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add user search history");
                return new UserSearchHistory();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var deleted = await repository.DeleteAsync(id);
                if (deleted)
                {
                    await repository.SaveChangesAsync();
                }
                return deleted;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete user search history with id {Id}", id);
                return false;
            }
        }

        public async Task<List<UserSearchHistory>> GetAllAsync()
        {
            try
            {
                return await repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get all user search histories");
                return new List<UserSearchHistory>();
            }
        }

        public async Task<UserSearchHistory?> GetByIdAsync(int id)
        {
            try
            {
                return await repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get user search history by id {Id}", id);
                return null;
            }
        }

        public async Task<List<UserSearchHistory>> GetByUserIdAsync(string userId)
        {
            try
            {
                return await repository.GetByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get user search history by user id {UserId}", userId);
                return new List<UserSearchHistory>();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to save changes in search service");
                return false;
            }
        }

        public async Task<List<UserSearchHistory>> GetTopGlobalSearchesAsync(int topN = 5)
        {
            try
            {
                return await repository.GetTopGlobalSearchesAsync(topN);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to get top {TopN} global search histories", topN);
                return new List<UserSearchHistory>();
            }
        }

    }
}
