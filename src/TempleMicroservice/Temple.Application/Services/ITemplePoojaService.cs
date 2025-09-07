using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITemplePoojaService
    {
        Task<bool> CreateAsync(TemplePooja pooja);
        Task<bool> UpdateAsync(TemplePooja pooja);
        Task<bool> DeleteAsync(int id);
        Task<TemplePooja?> GetByIdAsync(int id);
        Task<IEnumerable<TemplePooja>> GetAllAsync();
    }

}
