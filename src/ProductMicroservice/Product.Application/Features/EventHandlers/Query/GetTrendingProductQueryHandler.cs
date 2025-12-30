using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetTrendingProductQueryHandler : IRequestHandler<GetTrendingProductQuery, Result>
    {
        private readonly ILoggerService<GetTrendingProductQueryHandler> logger;
        private readonly IProductService productService;

        public GetTrendingProductQueryHandler(ILoggerService<GetTrendingProductQueryHandler> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        public async Task<Result> Handle(GetTrendingProductQuery request, CancellationToken cancellationToken)
        {
            dynamic? result = null;
            if(request.Scid is not null)
            {
                result = await productService.GetSubcategoryTrendingAsync(request.Scid, request.PageNumber);
            }
            else
            {
                result = await productService.GetTrendingProdcutsAsync(request.PageNumber);
            }

            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Products not found"));
            }

            return Result.Success(result);
        }
    }
}