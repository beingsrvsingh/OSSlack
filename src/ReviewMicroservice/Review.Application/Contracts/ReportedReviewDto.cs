using Review.Domain.Enum;

namespace Review.Application.Contracts
{
    public class ReportedReviewDto
    {
        public int ReportId { get; set; }
        public int ReviewId { get; set; }
        public required string ReportedByUserId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public ReportStatus Status { get; set; }
    }
}