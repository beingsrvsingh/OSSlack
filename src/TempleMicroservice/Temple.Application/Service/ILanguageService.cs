using Temple.Domain.Entities;

namespace Temple.Application.Service
{

    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(int id);
        Task<Language> CreateAsync(Language language);
        Task<bool> UpdateAsync(Language language);
        Task<bool> DeleteAsync(int id);
    }
}