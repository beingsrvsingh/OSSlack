
namespace Order.Application.Contracts
{
    public class OrderShippingAddressDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string RecipientName { get; set; } = null!;
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}