using Address.Domain.Enums;

namespace Address.Application.Contracts
{
    public class AddressDto
    {
        public string? Name { get; set; }
        public string? Label { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = "India";
        public string Pincode { get; set; } = null!;
        public string? Landmark { get; set; }
        public string? PhoneNumber { get; set; }
        public string AddressType { get; set; } = AddressOwnerType.Home.ToString();
        public bool IsDefault { get; set; }
    }

}