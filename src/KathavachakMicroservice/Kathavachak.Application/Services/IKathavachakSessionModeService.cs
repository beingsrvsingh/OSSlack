using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakSessionModeService
    {
        Task<IEnumerable<KathavachakSessionMode>> GetAllAsync();
        Task<KathavachakSessionMode?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakSessionMode entity);
        Task<bool> UpdateAsync(KathavachakSessionMode entity);
        Task<bool> DeleteAsync(int id);
    }
}
