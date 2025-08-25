
using Address.Domain.Entities;

namespace Address.Application.Contracts
{
    public class ShippingInfoDto
    {
        public string RecipientName { get; set; } = null!;
        public string AddressLine1 { get; set; }= null!;
        public string? AddressLine2 { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required string Country { get; set; } 
        public required string PhoneNumber { get; set; }
        public string? Landmark { get; set; }

        public static ShippingInfoDto ToResponseDto(AddressEntity address)
        {
            return new ShippingInfoDto
            {
                RecipientName = address.Name ?? string.Empty,  // or you can default to "N/A"
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address?.AddressLine2 ?? "",
                City = address!.City,
                State = address.State,
                PostalCode = address.Pincode,
                Country = address.Country,
                PhoneNumber = address.PhoneNumber ?? "",
                Landmark = address.Landmark,

            };
        }
    }

}