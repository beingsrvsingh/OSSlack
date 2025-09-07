using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakMediaService
    {
        Task<IEnumerable<KathavachakMedia>> GetAllAsync();
        Task<KathavachakMedia?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakMedia entity);
        Task<bool> UpdateAsync(KathavachakMedia entity);
        Task<bool> DeleteAsync(int id);
    }

}
