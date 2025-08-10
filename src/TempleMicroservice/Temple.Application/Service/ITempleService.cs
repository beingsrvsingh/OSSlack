using Temple.Application.Features.Commands;
using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;

namespace Temple.Application.Service
{
    public interface ITempleService
    {
        Task<IEnumerable<TempleMaster>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<TempleMaster?> GetByIdAsync(int astrologerId);
        Task<TempleMaster?> GetByUserIdAsync(string userId);
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> CreateAsync(CreateTempleCommand command);
        Task<bool> UpdateAsync(UpdateTempleCommand command);
        Task<bool> DeleteAsync(int astrologerId);

        Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds);
        Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds);

        Task<IEnumerable<TempleMaster>> SearchAsync(string? language = null, string? expertise = null, ConsultationMode? consultationMode = null, bool? isActive = true, int page = 1, int pageSize = 20);
    }
}