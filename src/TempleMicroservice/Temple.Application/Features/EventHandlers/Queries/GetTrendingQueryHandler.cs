using MediatR;
using Temple.Application.Features.Query;
using Temple.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Query
{
    public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, Result>
    {
        private readonly ILoggerService<GetTrendingQueryHandler> logger;
        private readonly ITempleService templeService;

        public GetTrendingQueryHandler(ILoggerService<GetTrendingQueryHandler> logger, ITempleService templeService)
        {
            this.logger = logger;
            this.templeService = templeService;
        }

        public async Task<Result> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
        {
            var result = await templeService.GetSubcategoryTrendingAsync(request.Scid, request.Records);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Products not found"));
            }

            return Result.Success(result);
        }
    }
}