using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingProductQueryHandler : IRequestHandler<GetTrendingProductQuery, Result>
    {
        private readonly ILoggerService<GetTrendingProductQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingProductQueryHandler(ILoggerService<GetTrendingProductQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await aggregatorService.GetTrendingProductAsync(1, 5, cancellationToken);

                if (result is null)
                {
                    logger.LogInfo("No trending Product data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Product.");
                throw;
            }
        }
    }
}
