
using System.Text.Json.Serialization;

namespace CartMicroservice.Application.Contracts
{
    public class AddCartItem
    {
        [JsonPropertyName("product_variant_id")]
        public int ProductVariantId { get; set; }

        [JsonPropertyName("product_type")]
        public string ProductType { get; set; }

        [JsonPropertyName("product_name")]
        public string ProductName { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }
    }

    public class UpdateCartItem
    {
        [JsonPropertyName("cart_id")]
        public int CartId { get; set; }

        [JsonPropertyName("product_variant_id")]
        public int ProductVariantId { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }
    }

    public class AddCartDto
    {
        [JsonPropertyName("user_id")]
        public required string UserId { get; set; }

        [JsonPropertyName("cost")]
        public double Amount { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }

        [JsonPropertyName("cart_items")]
        public List<AddCartItem> CartItems { get; set; } = new List<AddCartItem>();
    }

    public class UpdateCartDto
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("cost")]
        public double Amount { get; set; }

        [JsonPropertyName("discount")]
        public double Discount { get; set; }

        [JsonPropertyName("tax")]
        public double Tax { get; set; }

        [JsonPropertyName("cart_items")]
        public List<UpdateCartItem> CartItems { get; set; } = new List<UpdateCartItem>();
    }

}