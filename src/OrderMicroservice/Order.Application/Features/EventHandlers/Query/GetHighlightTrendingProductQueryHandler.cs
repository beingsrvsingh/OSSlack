using MediatR;
using Order.Application.Features.Query;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Query
{
    public class GetHighlightTrendingProductQueryHandler : IRequestHandler<GetHighlightTrendingProductQuery, Result>
    {
        private readonly ILoggerService<GetHighlightTrendingProductQueryHandler> logger;
        private readonly IOrderItemService orderItemService;

        public GetHighlightTrendingProductQueryHandler(ILoggerService<GetHighlightTrendingProductQueryHandler> logger, IOrderItemService orderItemService){
            this.logger = logger;
            this.orderItemService = orderItemService;
        }

        public async Task<Result> Handle(GetHighlightTrendingProductQuery request, CancellationToken cancellationToken)
        {
            var result = await orderItemService.GetTrendingProductsByCategoryAsync(request.Cid, request.Records);
            if (result is null)
            {
                return Result.Failure(new FailureResponse("NOT_FOUND", "Products not found"));
            }

            return Result.Success(result);
        }
    }
}