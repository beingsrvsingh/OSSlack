using MediatR;
using Pooja.Application.Features.Query;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Query
{
    public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, Result>
    {
        private readonly ILoggerService<GetTrendingQueryHandler> logger;
        private readonly IPoojaService _poojaService;

        public GetTrendingQueryHandler(ILoggerService<GetTrendingQueryHandler> logger, IPoojaService poojaService)
        {
            this.logger = logger;
            this._poojaService = poojaService;
        }

        public async Task<Result> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
        {
            dynamic? result = null;
            if (request.Scid is not null)
            {
                result = await _poojaService.GetSubcategoryTrendingAsync(request.Scid, request.PageNumber);
            }
            else
            {
                result = await _poojaService.GetTrendingProdcutsAsync(request.PageNumber);
            }
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Pooja not found"));
            }

            return Result.Success(result);
        }
    }
}