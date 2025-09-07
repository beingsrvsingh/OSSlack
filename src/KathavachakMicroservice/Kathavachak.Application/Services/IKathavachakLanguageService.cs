using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakLanguageService
    {
        Task<IEnumerable<KathavachakLanguage>> GetAllAsync();
        Task<KathavachakLanguage?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakLanguage entity);
        Task<bool> UpdateAsync(KathavachakLanguage entity);
        Task<bool> DeleteAsync(int id);
    }
}
