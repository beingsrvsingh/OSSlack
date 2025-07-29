using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class SupportResolveReportCommand : IRequest<Result>
    {
        public int ReviewId { get; set; }
        public required string SupportUserId { get; set; }
        public int ReportId { get; set; }
        public string ResolutionNote { get; set; } = string.Empty;
    }
}