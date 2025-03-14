using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewActionCommand : IRequest<Result>
    {
        public required string ReviewId { get; set; }
        public required string ReportId { get; set; }
        public required string ProductId { get; set; }
        public required bool IsReviewed { get; set; }

        public required string ReviewedBy { get; set; }

        public required DateTime ReviewedDate { get; set; }

        public required string ReviewReason { get; set; }
    }
}
