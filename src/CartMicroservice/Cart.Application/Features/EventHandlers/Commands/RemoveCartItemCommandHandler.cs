using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Entities;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Commands
{
    public class RemoveCartItemCommandHandler : IRequestHandler<RemoveCartItemCommand, Result>
{
    private readonly ICartService _cartService;
    private readonly ILoggerService<RemoveCartItemCommandHandler> _logger;

    public RemoveCartItemCommandHandler(ICartService cartService, ILoggerService<RemoveCartItemCommandHandler> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }

    public async Task<Result> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
                var cart = await _cartService.GetCartByProductIdAsync(request.productId);
                bool success = false;                

                if (cart.CartItems.Count == 1)
                {
                    success = await _cartService.RemoveCartAsync(cart.Id);
                }
                else
                {
                    success = await _cartService.RemoveCartItemAsync(request.productId);
                }

                    return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to remove cart item"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in RemoveCartItemCommandHandler: {ex.Message}", ex);
            return Result.Failure(new FailureResponse("Error", "Exception occurred"));
        }
    }
}
}