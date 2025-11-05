using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace AstrologerMicroservice.Domain.Repositories
{
    public interface IAstrologerRepository : IRepository<AstrologerMaster>
    {
        Task<AstrologerMaster?> GetAstrologerWithLanguagesAndExpertisesAsync(int id);
        Task<IEnumerable<AstrologerMaster>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<(List<AstrologerSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}