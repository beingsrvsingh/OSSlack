using MediatR;
using Order.Application.Features.Query;
using Order.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Order.Application.Features.EventHandlers.Query
{
    public class GetTrendingProductQueryHandler : IRequestHandler<GetTrendingProductQuery, Result>
    {
        private readonly ILoggerService<GetTrendingProductQueryHandler> logger;
        private readonly IOrderItemService orderItemService;

        public GetTrendingProductQueryHandler(ILoggerService<GetTrendingProductQueryHandler> logger, IOrderItemService orderItemService){
            this.logger = logger;
            this.orderItemService = orderItemService;
        }

        public async Task<Result> Handle(GetTrendingProductQuery request, CancellationToken cancellationToken)
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