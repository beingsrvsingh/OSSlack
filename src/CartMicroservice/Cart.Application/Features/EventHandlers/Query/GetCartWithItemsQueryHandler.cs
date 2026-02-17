using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Query;
using CartMicroservice.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Query
{
    public class GetCartWithItemsQueryHandler : IRequestHandler<GetCartWithItemsQuery, Result>
{
    private readonly ICartService _cartService;
    private readonly ILoggerService<GetCartWithItemsQueryHandler> _logger;

    public GetCartWithItemsQueryHandler(ICartService cartService, ILoggerService<GetCartWithItemsQueryHandler> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<Result> Handle(GetCartWithItemsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var cart = await _cartService.GetCartWithItemsAsync(request.CartId);
            if (cart is null)
                return Result.Success("No Carts Founds");

            var dto = cart.Adapt<CartResponseDto>();
            return Result.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetCartWithItemsQueryHandler: {ex.Message}", ex);
            return Result.Failure(new FailureResponse("Error", "Failed to get cart with items"));
        }
    }
}
}