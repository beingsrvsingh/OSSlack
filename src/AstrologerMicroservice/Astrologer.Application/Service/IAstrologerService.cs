using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;

namespace AstrologerMicroservice.Application.Service
{
    public interface IAstrologerService
    {
        Task<IEnumerable<Astrologer>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<Astrologer?> GetByIdAsync(int astrologerId);
        Task<Astrologer?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Astrologer>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> CreateAsync(CreateAstrologerCommand command);
        Task<bool> UpdateAsync(UpdateAstrologerCommand command);
        Task<bool> DeleteAsync(int astrologerId);

        Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds);
        Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds);

        Task<IEnumerable<Astrologer>> SearchAsync(string? language = null, string? expertise = null, ConsultationModeType? consultationMode = null, bool? isActive = true, int page = 1, int pageSize = 20);
    }
}