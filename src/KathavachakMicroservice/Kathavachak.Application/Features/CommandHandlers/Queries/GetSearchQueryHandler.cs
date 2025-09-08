using MediatR;
using Kathavachak.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly IKathavachakService kathavachakService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, IKathavachakService kathavachakService)
        {
            this.logger = logger;
            this.kathavachakService = kathavachakService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await kathavachakService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || searchResult.Results == null || !searchResult.Results.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            return Result.Success(searchResult);
        }

    }
}
