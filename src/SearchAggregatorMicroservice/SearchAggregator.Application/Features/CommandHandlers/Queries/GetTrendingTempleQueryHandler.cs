using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingTempleQueryHandler : IRequestHandler<GetTrendingTempleQuery, Result>
    {
        private readonly ILoggerService<GetTrendingTempleQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingTempleQueryHandler(ILoggerService<GetTrendingTempleQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingTempleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await aggregatorService.GetTrendingTempleAsync(1, 5, cancellationToken);

                if (result is null)
                {
                    logger.LogInfo("No trending Temple data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Temple.");
                throw;
            }
        }
    }
}
