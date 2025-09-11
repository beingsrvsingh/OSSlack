using Temple.Domain.Entities;
using Shared.Domain.Repository;
using TempleMicroservice.Domain.Entities;

namespace Temple.Domain.Repositories
{
    public interface ITempleRepository : IRepository<TempleMaster>
    {
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<TempleMaster?> GetByIdWithDetailsAsync(int id);

        Task<(List<SearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}