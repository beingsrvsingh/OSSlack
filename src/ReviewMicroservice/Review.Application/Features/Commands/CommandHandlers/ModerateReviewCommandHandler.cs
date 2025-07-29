using MediatR;
using Review.Application.Features.Commands;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class ModerateReviewCommandHandler : IRequestHandler<ReviewModerationCommand, Result>
{
    private readonly IReviewService _reviewService;
    private readonly ILoggerService<ModerateReviewCommandHandler> _logger;

    public ModerateReviewCommandHandler(ILoggerService<ModerateReviewCommandHandler> logger, IReviewService reviewService)
    {
        _reviewService = reviewService;
        _logger = logger;
    }

    public async Task<Result> Handle(ReviewModerationCommand request, CancellationToken cancellationToken)
    {
        var success = await _reviewService.ModerateReviewAsync(request);
        if (!success)
        {
            _logger.LogWarning("Moderation failed or review not found: {ReviewId}", request.ReviewId);
            return Result.Failure(new FailureResponse("ModerationFailed", "Moderation failed or review not found"));
        }

        return Result.Success(true);
    }
}
