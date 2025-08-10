using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;
using Shared.Domain.Repository;

namespace AstrologerMicroservice.Domain.Repositories
{
    public interface IAstrologerRepository : IRepository<Astrologer>
    {
        Task<Astrologer?> GetAstrologerWithLanguagesAndExpertisesAsync(int id);
        Task<IEnumerable<Astrologer>> GetAvailableAsync(DateTime date, string language, string expertise);
        Task<IEnumerable<Astrologer>> GetAllAsync(int page = 1, int pageSize = 20);
        Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds);
        Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds);
        Task<IEnumerable<Astrologer>> SearchAsync(
        string? language = null,
        string? expertise = null,
        ConsultationMode? consultationMode = null,
        bool? isActive = true,
        int page = 1,
        int pageSize = 20);
    }
}