
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

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }
    }

    public class UpdateCartDto
    {

        [JsonPropertyName("product_variant_id")]
        public int ProductVariantId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

}