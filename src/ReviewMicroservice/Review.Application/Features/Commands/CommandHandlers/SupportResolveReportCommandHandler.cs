using MediatR;
using Review.Application.Features.Commands;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class SupportResolveReportCommandHandler : IRequestHandler<SupportResolveReportCommand, Result>
{
    private readonly IReviewService _reviewService;
    private readonly ILoggerService<SupportResolveReportCommandHandler> _logger;

    public SupportResolveReportCommandHandler(IReviewService reviewService, ILoggerService<SupportResolveReportCommandHandler> logger)
    {
        _reviewService = reviewService;
        _logger = logger;
    }

    public async Task<Result> Handle(SupportResolveReportCommand request, CancellationToken cancellationToken)
    {
        var resolved = await _reviewService.SupportResolveReportAsync(request);

        if (!resolved)
        {
            _logger.LogWarning("Failed to resolve report {ReportId} for review {ReviewId}", request.ReportId, request.ReviewId);
            return Result.Failure(new FailureResponse("ResolveFailed", "Report not found or cannot be resolved."));
        }

        return Result.Success("Report resolved successfully.");
    }
}
