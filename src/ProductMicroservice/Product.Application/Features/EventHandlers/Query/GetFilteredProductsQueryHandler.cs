using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQuery, Result>
    {
        private readonly ILoggerService<GetFilteredProductsQueryHandler> _logger;
        private readonly IProductService _productService;
        private readonly ICatalogService catalogService;
        private readonly IReviewService reviewService;

        public GetFilteredProductsQueryHandler(
            ILoggerService<GetFilteredProductsQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService,
            IReviewService reviewService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
            this.reviewService = reviewService;
        }

        public async Task<Result> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetFilteredProductsAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<ProductBySubCategoryResponseDto>());
            }
            return Result.Success(response);

        }


    }
}