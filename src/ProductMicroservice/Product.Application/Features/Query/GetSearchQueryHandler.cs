using MediatR;
using Product.Application.Services;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly IProductService productService;
        private readonly ICatalogService catalogService;
        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, IProductService productService, ICatalogService catalogService)
        {
            this.logger = logger;
            this.productService = productService;
            this.catalogService = catalogService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await productService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);            

            if (searchResult == null || searchResult.Results == null || !searchResult.Results.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            IEnumerable<BaseCatalogAttributeDto> attributes = Enumerable.Empty<BaseCatalogAttributeDto>();

            if (searchResult.Filters.MatchType == "Exact")
            {
                attributes = await catalogService.GetAttributesByCategoryId(Convert.ToInt32(searchResult.Results.FirstOrDefault()!.Cid), Convert.ToInt32(searchResult.Results.FirstOrDefault().Scid), false);
            }

            var dto = SearchResponseDto.FromEntityList(searchResult, attributes);

            return Result.Success(dto);
        }

    }
}
