using Kathavachak.Application.Features.Query;
using Kathavachak.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.EventHandlers.Query
{
    public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, Result>
    {
        private readonly ILoggerService<GetTrendingQueryHandler> logger;
        private readonly IKathavachakService _kathavachakService;

        public GetTrendingQueryHandler(ILoggerService<GetTrendingQueryHandler> logger, IKathavachakService kathavachakService)
        {
            this.logger = logger;
            this._kathavachakService = kathavachakService;
        }

        public async Task<Result> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
        {
            var result = await _kathavachakService.GetSubcategoryTrendingAsync(request.Scid, request.Records);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Astrologer not found"));
            }

            return Result.Success(result);
        }
    }
}