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
