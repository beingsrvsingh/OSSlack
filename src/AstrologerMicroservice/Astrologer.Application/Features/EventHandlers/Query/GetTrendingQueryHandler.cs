using AstrologerMicroservice.Application.Service;
using MediatR;
using Astrologer.Application.Features.Query;
using Astrologer.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.EventHandlers.Query
{
    public class GetTrendingQueryHandler : IRequestHandler<GetTrendingQuery, Result>
    {
        private readonly ILoggerService<GetTrendingQueryHandler> logger;
        private readonly IAstrologerService _astrologerService;

        public GetTrendingQueryHandler(ILoggerService<GetTrendingQueryHandler> logger, IAstrologerService astrologerService)
        {
            this.logger = logger;
            this._astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetTrendingQuery request, CancellationToken cancellationToken)
        {
            var result = await _astrologerService.GetSubcategoryTrendingAsync(request.Scid, request.Records);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Astrologer not found"));
            }

            return Result.Success(result);
        }
    }
}