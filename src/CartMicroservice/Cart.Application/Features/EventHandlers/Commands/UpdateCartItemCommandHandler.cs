using Cart.Application.Features.Commands;
using CartMicroservice.Application.Services;
using CartMicroservice.Domain.Core.Repositories;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Cart.Application.Features.EventHandlers.Commands
{
    public class UpdateCartItemCommandHandler : IRequestHandler<UpdateCartItemCommand, Result>
    {
        private readonly ILoggerService<UpdateCartItemCommandHandler> logger;
        private readonly ICartService cartService;

        public UpdateCartItemCommandHandler(ILoggerService<UpdateCartItemCommandHandler> logger, ICartService cartService)
        {
            this.logger = logger;
            this.cartService = cartService;
        }

        public async Task<Result> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = await cartService.GetCartByProductIdAsync(request.productVariantId);

            bool isUpdated = false;

            if (cart is null)
            {
                return Result.Failure(
                    new FailureResponse("ITEM_NOT_FOUND", "Cart item not found."));
            }

            isUpdated = await cartService.UpdateCartItemAsync(cart, request.productVariantId, request.Quantity);

            if(isUpdated && cart.CartItems.Any((c) => c.Quantity == 0))
            {
                isUpdated = await cartService.RemoveCartAsync(request.productVariantId);
            }

            if (isUpdated) {
                return Result.Success();
            }

            return Result.Failure("Something went wrong.");
        }
    }
}
