using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductsByIdAndCategoryIdQueryHandler : IRequestHandler<GetProductsByIdAndCategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetProductsByIdAndCategoryIdQueryHandler> logger;
        private readonly IProductService productService;

        public GetProductsByIdAndCategoryIdQueryHandler(ILoggerService<GetProductsByIdAndCategoryIdQueryHandler> logger, IProductService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }

        public async Task<Result> Handle(GetProductsByIdAndCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var result = await productService.GetProductsByIdAndCategoryIdAsync(request.Pids, request.Cid);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Products not found"));
            }
            var dtos = result.Select((p => new ProductSummaryDto()
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId,
                ImageUrl = p.ThumbnailUrl,
                Cost = (double)p.Price,
                Name = p.Name,
                Rating = p.Rating,
                Reviews = p.Reviews
            }));

            return Result.Success(dtos);
        }
    }
}