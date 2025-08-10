using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;
using Shared.Domain.Repository;

namespace Temple.Domain.Repositories
{
    public interface ITempleRepository : IRepository<TempleMaster>
    {
        Task<TempleMaster?> GetAstrologerWithLanguagesAndExpertisesAsync(int id);
        Task<IEnumerable<TempleMaster>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds);
        Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds);
        Task<IEnumerable<TempleMaster>> SearchAsync(
        string? language = null,
        string? expertise = null,
        ConsultationMode? consultationMode = null,
        bool? isActive = true,
        int page = 1,
        int pageSize = 20);
    }
}