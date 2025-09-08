using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakCategoryService
    {
        Task<IEnumerable<KathavachakExpertise>> GetAllAsync();
        Task<KathavachakExpertise?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakExpertise entity);
        Task<bool> UpdateAsync(KathavachakExpertise entity);
        Task<bool> DeleteAsync(int id);
    }

}
