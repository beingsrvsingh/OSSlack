using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Query;
using CartMicroservice.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Query
{
    public class GetCartItemsByCartIdQueryHandler : IRequestHandler<GetCartItemsByCartIdQuery, Result>
    {
        private readonly ICartItemService _cartItemService;
        private readonly ILoggerService<GetCartItemsByCartIdQueryHandler> _logger;

        public GetCartItemsByCartIdQueryHandler(ICartItemService cartItemService, ILoggerService<GetCartItemsByCartIdQueryHandler> logger)
        {
            _cartItemService = cartItemService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetCartItemsByCartIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var items = await _cartItemService.GetItemsByCartIdAsync(request.CartId);
                var dtos = items.Adapt<IEnumerable<CartItemDto>>();
                return Result.Success(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception in GetCartItemsByCartIdQueryHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to retrieve cart items"));
            }
        }
    }
}