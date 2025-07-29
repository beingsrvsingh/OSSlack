using MediatR;
using Review.Domain.Enum;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewReportCommand : IRequest<Result>
    {
        public required int ReviewId { get; set; }
        public required string ReportedByUserId { get; set; }
        public string Reason { get; set; } = string.Empty;        
        public DateTime ReportedAt { get; set; }
        public ReportStatus Status { get; set; }
    }
}