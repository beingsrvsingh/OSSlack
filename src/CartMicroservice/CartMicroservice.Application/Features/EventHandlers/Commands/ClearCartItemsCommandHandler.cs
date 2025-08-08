using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Commands
{
    public class ClearCartItemsCommandHandler : IRequestHandler<ClearCartItemsCommand, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<ClearCartItemsCommandHandler> _logger;

        public ClearCartItemsCommandHandler(ICartService cartService, ILoggerService<ClearCartItemsCommandHandler> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<Result> Handle(ClearCartItemsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _cartService.ClearCartItemsAsync(request.CartId);
                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to clear cart items"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ClearCartItemsCommandHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred"));
            }
        }
    }
}