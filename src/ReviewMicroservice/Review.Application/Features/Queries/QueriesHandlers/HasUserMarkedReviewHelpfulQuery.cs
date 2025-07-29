using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class HasUserMarkedReviewHelpfulQueryHandler : IRequestHandler<HasUserMarkedReviewHelpfulQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<HasUserMarkedReviewHelpfulQueryHandler> _logger;

        public HasUserMarkedReviewHelpfulQueryHandler(ILoggerService<HasUserMarkedReviewHelpfulQueryHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(HasUserMarkedReviewHelpfulQuery request, CancellationToken cancellationToken)
        {
            var hasMarked = await _reviewService.HasUserMarkedReviewHelpfulAsync(request.ReviewId, request.UserId);

            if (hasMarked)
            {
                _logger.LogInfo("User {UserId} has already marked review {ReviewId} as helpful.", request.UserId, request.ReviewId);
                return Result.Success("You have already marked this review as helpful.");
            }

            return Result.Success("You have not marked this review as helpful yet.");
        }
    }

}