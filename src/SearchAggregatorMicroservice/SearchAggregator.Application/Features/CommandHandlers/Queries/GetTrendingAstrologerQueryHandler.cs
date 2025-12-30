using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingAstrologerQueryHandler : IRequestHandler<GetTrendingAstrologerQuery, Result>
    {
        private readonly ILoggerService<GetTrendingAstrologerQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingAstrologerQueryHandler(ILoggerService<GetTrendingAstrologerQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingAstrologerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await aggregatorService.GetTrendingAstrologerAsync(1, 5, cancellationToken);

                if (result is null)
                {
                    logger.LogInfo("No trending Astrologer data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Astrologer.");
                throw;
            }
        }
    }
}
