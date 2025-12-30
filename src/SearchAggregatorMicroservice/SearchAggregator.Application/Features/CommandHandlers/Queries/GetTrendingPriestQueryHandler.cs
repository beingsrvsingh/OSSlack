using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingPriestQueryHandler : IRequestHandler<GetTrendingPriestQuery, Result>
    {
        private readonly ILoggerService<GetTrendingPriestQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingPriestQueryHandler(ILoggerService<GetTrendingPriestQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingPriestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await aggregatorService.GetTrendingPriestAsync(1, 5, cancellationToken);

                if (result is null)
                {
                    logger.LogInfo("No trending Priest data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Priest.");
                throw;
            }
        }
    }
}
