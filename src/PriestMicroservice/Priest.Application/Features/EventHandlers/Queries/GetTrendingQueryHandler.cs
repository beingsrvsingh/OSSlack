using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Query
{
    public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, Result>
    {
        private readonly ILoggerService<GetTrendingQueryHandler> logger;
        private readonly IPriestService _priestService;

        public GetTrendingQueryHandler(ILoggerService<GetTrendingQueryHandler> logger, IPriestService priestService)
        {
            this.logger = logger;
            this._priestService = priestService;
        }

        public async Task<Result> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
        {
            var result = await _priestService.GetSubcategoryTrendingAsync(request.Scid, request.Records);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Priest not found"));
            }

            return Result.Success(result);
        }
    }
}