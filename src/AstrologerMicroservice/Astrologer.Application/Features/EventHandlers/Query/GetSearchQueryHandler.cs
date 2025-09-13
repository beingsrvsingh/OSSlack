using Astrologer.Application.Services;
using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Common.Contracts;
using Shared.Application.Contracts;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly ICatalogService catalogService;
        private readonly IAstrologerService astrologerService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, ICatalogService catalogService, IAstrologerService astrologerService)
        {
            this.logger = logger;
            this.catalogService = catalogService;
            this.astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await astrologerService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

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
