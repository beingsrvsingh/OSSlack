using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Contracts;

namespace AstrologerMicroservice.Application.Service
{
    public interface IAstrologerService
    {
        Task<IEnumerable<AstrologerMaster>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<CatalogResponseDto?> GetByIdAsync(int astrologerId);
        Task<AstrologerMaster?> GetByUserIdAsync(string userId);
        Task<IEnumerable<AstrologerMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> CreateAsync(CreateAstrologerCommand command);
        Task<bool> UpdateAsync(UpdateAstrologerCommand command);
        Task<bool> DeleteAsync(int astrologerId);
        Task<ProductSearchRawResultDto> SearchAsync(string query, int page, int pageSize, CancellationToken cancellationToken);
    }
}