using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleLocalizedTextService
    {
        Task<bool> CreateAsync(TempleLocalizedText text);
        Task<bool> UpdateAsync(TempleLocalizedText text);
        Task<bool> DeleteAsync(int id);
        Task<TempleLocalizedText?> GetByIdAsync(int id);
        Task<IEnumerable<TempleLocalizedText>> GetAllAsync();
    }

}
