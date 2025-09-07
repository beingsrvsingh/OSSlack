using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleAartiService
    {
        Task<bool> CreateAsync(TempleAarti aarti);
        Task<bool> UpdateAsync(TempleAarti aarti);
        Task<bool> DeleteAsync(int id);
        Task<TempleAarti?> GetByIdAsync(int id);
        Task<IEnumerable<TempleAarti>> GetAllAsync();
    }

}
