using AstrologerMicroservice.Domain.Entities;

namespace AstrologerMicroservice.Application.Service
{

    public interface ILanguageService
    {
        Task<IEnumerable<LanguageMaster>> GetAllAsync();
        Task<LanguageMaster?> GetByIdAsync(int id);
        Task<LanguageMaster> CreateAsync(LanguageMaster language);
        Task<bool> UpdateAsync(LanguageMaster language);
        Task<bool> DeleteAsync(int id);
    }
}