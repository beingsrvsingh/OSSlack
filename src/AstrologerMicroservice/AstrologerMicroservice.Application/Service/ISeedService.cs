

namespace AstrologerMicroservice.Application.Service
{
    public interface ISeedService
    {
        Task<bool> SeedAstrologerLanguagesAsync();
        Task<bool> SeedAstrologerExpertiesAsync();
    }
}