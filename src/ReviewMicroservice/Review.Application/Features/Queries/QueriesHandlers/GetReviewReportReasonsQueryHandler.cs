using MediatR;
using Review.Application.Features.Queries.Review.Application.Features.Queries;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class GetReviewReportReasonsQueryHandler : IRequestHandler<GetReviewReportReasonsQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<GetReviewReportReasonsQueryHandler> _logger;

        public GetReviewReportReasonsQueryHandler(ILoggerService<GetReviewReportReasonsQueryHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetReviewReportReasonsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var reasons = await _reviewService.GetReportReasonsAsync();
                return Result.Success(reasons);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving review report reasons");
                return Result.Failure(new FailureResponse("ServerError", "Failed to fetch report reasons"));
            }
        }
    }

}