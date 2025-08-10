using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddOrUpdateCartItemCommandHandler : IRequestHandler<AddOrUpdateCartItemCommand, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<AddOrUpdateCartItemCommandHandler> _logger;

        public AddOrUpdateCartItemCommandHandler(ICartService cartService, ILoggerService<AddOrUpdateCartItemCommandHandler> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddOrUpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var item = request.CartItem.Adapt<CartItem>();
                var success = await _cartService.AddOrUpdateCartItemAsync(item);
                return success ? Result.Success() : Result.Failure(new FailureResponse("Error", "Failed to add or update cart item"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddOrUpdateCartItemCommandHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Exception occurred"));
            }
        }
    }
}