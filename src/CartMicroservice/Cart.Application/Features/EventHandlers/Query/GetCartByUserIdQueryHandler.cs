using Cart.Application.Contracts;
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
                {
                    var cartResponse = new CartResponseDto
                    {
                        CartItems = new List<CartItemDto>(),
                        BillItems = new List<BillItemDto>(),
                        GrandTotal = 0.ToString()
                    };
                    return Result.Success(cartResponse);
                }

                var cartItemsDto = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductId = ci.ProductVariantId.ToString(),
                    Name = ci.ItemNameSnapshot,
                    Quantity = ci.Quantity,
                    Price = ci.PriceSnapshot,
                    ImageUrl = ci.ImageUrl,
                    AdditionalFees = ci.AdditionalFees,
                    ProviderType = ci.ProviderType
                }).ToList();

                var billItemsDto = new List<BillItemDto>
                                    {
                                        new BillItemDto
                                        {
                                            Key = "item_total",
                                            Label = "Items Total",
                                            Value = cart.Subtotal.ToString(),
                                            Type = "charge",
                                            Tooltip = "Sum of all item prices"
                                        },
                                        new BillItemDto
                                        {
                                            Key = "coupon",
                                            Label = "Coupon Discount",
                                            Value = cart.TotalDiscount.ToString(),
                                            Type = "discount"
                                        },
                                        new BillItemDto
                                        {
                                            Key = "tax",
                                            Label = "Tax",
                                            Value = cart.TotalTax.ToString(),
                                            Type = "charge"
                                        },
                                        new BillItemDto
                                        {
                                            Key = "platform_fee",
                                            Label = "Platform Fee",
                                            Value = cart.PlatformFee.ToString(),
                                            Type = "charge"
                                        },
                                    };

                // Build the response DTO
                var dto = new CartResponseDto
                {
                    CartItems = cartItemsDto,
                    BillItems = billItemsDto,
                    GrandTotal = cart.TotalAmount.ToString()
                };

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