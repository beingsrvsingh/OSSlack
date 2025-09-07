using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakCategoryService
    {
        Task<IEnumerable<KathavachakCategory>> GetAllAsync();
        Task<KathavachakCategory?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakCategory entity);
        Task<bool> UpdateAsync(KathavachakCategory entity);
        Task<bool> DeleteAsync(int id);
    }

}
