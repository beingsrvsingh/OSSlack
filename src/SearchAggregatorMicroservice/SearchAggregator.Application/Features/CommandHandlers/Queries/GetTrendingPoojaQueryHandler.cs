using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingPoojaQueryHandler : IRequestHandler<GetTrendingPoojaQuery, Result>
    {
        private readonly ILoggerService<GetTrendingPoojaQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingPoojaQueryHandler(ILoggerService<GetTrendingPoojaQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingPoojaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await aggregatorService.GetTrendingPoojaAsync(1, 5, cancellationToken);

                if (result is null)
                {
                    logger.LogInfo("No trending Pooja data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Pooja.");
                throw;
            }
        }
    }
}
