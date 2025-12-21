using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly ICatalogService catalogService;
        private readonly IPriestService priestService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, ICatalogService catalogService, IPriestService priestService)
        {
            this.logger = logger;
            this.catalogService = catalogService;
            this.priestService = priestService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await priestService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || !searchResult.Items.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            return Result.Success(searchResult);
        }

    }

}
