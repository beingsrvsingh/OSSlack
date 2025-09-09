using PriestMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace Priest.Domain.Core.Repository
{
    public interface IPriestRepository : IRepository<PriestMaster>
    {
        Task<(List<SearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
