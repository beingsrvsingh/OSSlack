using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakTopicService
    {
        Task<IEnumerable<KathavachakTopic>> GetAllAsync();
        Task<KathavachakTopic?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakTopic entity);
        Task<bool> UpdateAsync(KathavachakTopic entity);
        Task<bool> DeleteAsync(int id);
    }
}
