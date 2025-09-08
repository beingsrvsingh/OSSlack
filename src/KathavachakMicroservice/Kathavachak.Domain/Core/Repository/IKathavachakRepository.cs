using Kathavachak.Domain.Entities;
using Shared.Domain.Repository;

namespace Kathavachak.Domain.Core.Repository
{
    public interface IKathavachakRepository : IRepository<KathavachakMaster>
    {
        Task<(List<KathavachakSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
