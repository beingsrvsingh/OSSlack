using MediatR;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly ICatalogService catalogService;
        private readonly ITempleService templeService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, ICatalogService catalogService, ITempleService templeService)
        {
            this.logger = logger;
            this.catalogService = catalogService;
            this.templeService = templeService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await templeService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || searchResult.Results == null || !searchResult.Results.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            IEnumerable<BaseCatalogAttributeDto> attributes = Enumerable.Empty<BaseCatalogAttributeDto>();

            if (searchResult.Filters.MatchType == "Exact")
            {
                attributes = await catalogService.GetAttributesByCategoryId(Convert.ToInt32(searchResult.Results.FirstOrDefault()!.CategoryId), Convert.ToInt32(searchResult.Results.FirstOrDefault().SubCategoryId), false);
            }

            var dto = SearchResponseDto.FromEntityList(searchResult, attributes);

            return Result.Success(dto);
        }

    }
}
