using Address.Domain.Enums;

namespace Address.Application.Contracts
{
    public class CreateAddressDto
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
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? TimeZone { get; set; }
        public int OwnerId { get; set; }
        public AddressOwnerType OwnerType { get; set; } = AddressOwnerType.User;
        public int? CreatedBy { get; set; }
    }
}