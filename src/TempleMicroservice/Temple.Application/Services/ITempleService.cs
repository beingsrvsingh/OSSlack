using Shared.Application.Contracts;
using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleService
    {
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<TempleMaster?> GetByIdWithDetailsAsync(int id);
        Task<bool> CreateAsync(TempleMaster temple);
        Task<bool> UpdateAsync(TempleMaster temple);
        Task<bool> DeleteAsync(int id);
        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
