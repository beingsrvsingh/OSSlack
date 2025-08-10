using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Query;
using CartMicroservice.Application.Services;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace CartMicroservice.Application.Features.EventHandlers.Query
{
    public class GetCartByUserIdQueryHandler : IRequestHandler<GetCartByUserIdQuery, Result>
    {
        private readonly ICartService _cartService;
        private readonly ILoggerService<GetCartByUserIdQueryHandler> _logger;

        public GetCartByUserIdQueryHandler(ICartService cartService, ILoggerService<GetCartByUserIdQueryHandler> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _cartService.GetCartByUserIdAsync(request.UserId);
                if (cart is null)
                    return Result.Success("Not Items Found");

                var dto = cart.Adapt<CartDto>();
                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCartByUserIdQueryHandler: {ex.Message}", ex);
                return Result.Failure(new FailureResponse("Error", "Failed to get cart"));
            }
        }
    }
}