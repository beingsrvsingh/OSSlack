using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleExceptionService
    {
        Task<bool> CreateAsync(TempleException exception);
        Task<bool> UpdateAsync(TempleException exception);
        Task<bool> DeleteAsync(int id);
        Task<TempleException?> GetByIdAsync(int id);
        Task<IEnumerable<TempleException>> GetAllAsync();
    }

}
