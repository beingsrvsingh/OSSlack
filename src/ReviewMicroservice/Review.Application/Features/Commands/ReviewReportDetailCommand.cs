using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewReportDetailCommand : IRequest<Result>
    {
        public required string ReviewId { get; set; }
        public required string ReportId { get; set; }
        public required string ProductId { get; set; }
        public string? Message { get; set; }
    }
}
