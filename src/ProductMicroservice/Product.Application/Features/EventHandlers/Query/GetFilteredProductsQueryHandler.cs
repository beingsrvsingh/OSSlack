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

        public GetFilteredProductsQueryHandler(
            ILoggerService<GetFilteredProductsQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
        }

        public async Task<Result> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetFilteredProductsAsync(
                request.AttributeId,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.SortDescending
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<ProductBySubCategoryResponseDto>());
            }

            var attributes = await catalogService.GetAttributesByCategoryId(
                Convert.ToInt32(request.CategoryId),
                Convert.ToInt32(request.SubCategoryId)
            );

            var grouped = response
                .GroupBy(r => r.Id)
                .Select(group =>
                {
                    var entity = new ProductMaster
                    {
                        Id = group.Key,
                        Name = group.First().Name,
                        ThumbnailUrl = group.First().ThumbnailUrl,
                        Price = (decimal)group.First().Price,
                        CategoryId = group.First().CategoryId,
                        SubCategoryId = group.First().SubCategoryId,
                        AttributeValues = group
                            .Where(g => !string.IsNullOrEmpty(g.AttributeKey))
                            .Select(g => new ProductAttributeValue
                            {
                                AttributeKey = g.AttributeKey,
                                AttributeLabel = g.AttributeLabel,
                                Value = g.Value ?? string.Empty
                            }).ToList()
                    };

                    return ProductBySubCategoryResponseDto.FromEntity(entity, attributes);
                })
                .ToList();

            return Result.Success(grouped);
        }


    }
}