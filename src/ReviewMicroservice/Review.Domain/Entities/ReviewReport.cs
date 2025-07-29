using Review.Domain.Enum;

namespace Review.Domain.Entities
{
    public partial class ReviewReport
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string ReportedByUserId { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime ReportedAt { get; set; }
        public ReportStatus Status { get; set; }
        public string? ResolutionNote { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? ResolvedByUserId { get; set; }

        public Review Review { get; set; } = null!; // Navigation back to Review

    }
}