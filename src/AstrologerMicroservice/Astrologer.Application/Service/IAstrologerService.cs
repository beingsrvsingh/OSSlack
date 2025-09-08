using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;
using Shared.Application.Contracts;

namespace AstrologerMicroservice.Application.Service
{
    public interface IAstrologerService
    {
        Task<IEnumerable<AstrologerEntity>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<AstrologerEntity?> GetByIdAsync(int astrologerId);
        Task<AstrologerEntity?> GetByUserIdAsync(string userId);
        Task<IEnumerable<AstrologerEntity>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> CreateAsync(CreateAstrologerCommand command);
        Task<bool> UpdateAsync(UpdateAstrologerCommand command);
        Task<bool> DeleteAsync(int astrologerId);
        Task<SearchResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}