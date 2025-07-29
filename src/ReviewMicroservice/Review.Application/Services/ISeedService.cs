

namespace Review.Application.Services
{
    public interface ISeedService
    {
        Task<bool> SeedReviewReportReasonsAsync();
    }
}