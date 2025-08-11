using Temple.Domain.Entities;
using Shared.Domain.Repository;

namespace Temple.Domain.Repositories
{
    public interface ITempleRepository : IRepository<TempleMaster>
    {
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
    }
}