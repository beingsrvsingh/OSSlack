using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly ICatalogService catalogService;
        private readonly IPoojaService poojaService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, ICatalogService catalogService, IPoojaService poojaService)
        {
            this.logger = logger;
            this.catalogService = catalogService;
            this.poojaService = poojaService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await poojaService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || !searchResult.Items.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            return Result.Success(searchResult);
        }

    }
}
