using Kathavachak.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakService
    {
        Task<CatalogResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<KathavachakMaster>> GetAllAsync();
        Task<bool> CreateAsync(KathavachakMaster entity);
        Task<bool> UpdateAsync(KathavachakMaster entity);
        Task<bool> DeleteAsync(int id);
        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }

}
