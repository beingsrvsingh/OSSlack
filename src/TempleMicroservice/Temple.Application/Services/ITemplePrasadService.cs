using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITemplePrasadService
    {
        Task<bool> CreateAsync(TemplePrasad prasad);
        Task<bool> UpdateAsync(TemplePrasad prasad);
        Task<bool> DeleteAsync(int id);
        Task<TemplePrasad?> GetByIdAsync(int id);
        Task<IEnumerable<TemplePrasad>> GetAllAsync();
    }

}
