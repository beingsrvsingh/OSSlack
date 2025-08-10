using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
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
            var success = await _cartService.RemoveCartItemAsync(request.CartItemId);
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