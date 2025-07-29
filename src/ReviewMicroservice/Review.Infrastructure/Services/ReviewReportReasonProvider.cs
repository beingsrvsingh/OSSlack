using Review.Application.Services;
using Review.Domain.Entities;

namespace Review.Infrastructure.Services
{
    public class ReviewReportReasonProvider : IReviewReportReasonProvider
    {
        public IReadOnlyList<ReviewReportReason> GetDefaultReasons() => new List<ReviewReportReason>
    {
        new() { Title = "Spam", DisplayName = "Spam", Descriptions = "Irrelevant or promotional content", DisplayOrder = 1 },
        new() { Title = "Abusive", DisplayName = "Abusive Language", Descriptions = "Contains offensive or inappropriate content", DisplayOrder = 2 },
        new() { Title = "Fake", DisplayName = "Fake Review", Descriptions = "Misleading or dishonest review", DisplayOrder = 3 },
        new() { Title = "Other", DisplayName = "Other", Descriptions = "Other reason not listed", DisplayOrder = 4 }
    };
    }

}