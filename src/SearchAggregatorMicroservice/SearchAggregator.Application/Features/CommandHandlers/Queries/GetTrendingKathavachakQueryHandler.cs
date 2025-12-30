using MediatR;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTrendingKathavachakQueryHandler : IRequestHandler<GetTrendingKathavachakQuery, Result>
    {
        private readonly ILoggerService<GetTrendingKathavachakQueryHandler> logger;
        private readonly IAggregatorService aggregatorService;

        public GetTrendingKathavachakQueryHandler(ILoggerService<GetTrendingKathavachakQueryHandler> logger, IAggregatorService aggregatorService)
        {
            this.logger = logger;
            this.aggregatorService = aggregatorService;
        }

        public async Task<Result> Handle(GetTrendingKathavachakQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result =  await aggregatorService.GetTrendingKathavachakAsync(1, 5, cancellationToken);

                if (result is null){
                    logger.LogInfo("No trending Kathavachak data found.");
                    return Result.Failure("No trending data available.");
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error while fetching trending Kathavachak.");
                throw;
            }   
        }
    }
}
