using AstrologerMicroservice.Domain.Entities;
using Shared.Domain.Repository;

namespace AstrologerMicroservice.Domain.Repositories
{
    public interface IAstrologerRepository : IRepository<AstrologerEntity>
    {
        Task<AstrologerEntity?> GetAstrologerWithLanguagesAndExpertisesAsync(int id);
        Task<IEnumerable<AstrologerEntity>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<IEnumerable<AstrologerEntity>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<(List<AstrologerSearchRaw>, int)> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}