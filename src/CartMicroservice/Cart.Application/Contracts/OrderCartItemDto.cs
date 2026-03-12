using System.Text.Json;

namespace Cart.Application.Contracts
{
    public class OrderCartItemDto
    {
        public  int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public string? Sku { get; set; }

        public string ProductUrl { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime? PreferedDeliveryDate { get; set; }

        public string? ProductOptions { get; set; }
    }
}
