
using Cart.Application.Contracts;
using System.Text.Json.Serialization;

namespace CartMicroservice.Application.Contracts
{
    public class CartResponseDto
    {
        [JsonPropertyName("cart_items")]
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();

        [JsonPropertyName("bill_items")]
        public List<BillItemDto> BillItems { get; set; } = new List<BillItemDto>();

        [JsonPropertyName("grand_total")]
        public string GrandTotal { get; set; } = "0";
    }

}