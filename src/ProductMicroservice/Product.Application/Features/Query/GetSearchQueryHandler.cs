using MediatR;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly IProductService productService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await productService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || searchResult.Results == null || !searchResult.Results.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            return Result.Success(searchResult);
        }

    }
}
