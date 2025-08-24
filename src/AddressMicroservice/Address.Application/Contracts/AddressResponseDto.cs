using Address.Domain.Entities;
using Address.Domain.Enums;

namespace Address.Application.Contracts
{
    public class AddressResponseDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Label { get; set; }

        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Country { get; set; } = "India";

        public String Pincode { get; set; } = null!;

        public string? Landmark { get; set; }

        public string? PhoneNumber { get; set; }

        public string AddressType { get; set; } = AddressOwnerType.Home.ToString();

        public bool IsDefault { get; set; }

        public static AddressResponseDto ToResponseDto(AddressEntity entity)
        {
            return new AddressResponseDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Label = entity.Label,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                City = entity.City,
                State = entity.State,
                Country = entity.Country,
                Pincode = entity.Pincode,
                Landmark = entity.Landmark,
                PhoneNumber = entity.PhoneNumber,
                AddressType = entity.AddressType?.Name ?? "Other",
                IsDefault = entity.IsDefault
            };
        }

        public static List<AddressResponseDto> ToResponseDtoList(IEnumerable<AddressEntity> entities)
        {
            return entities.Select(ToResponseDto).ToList();
        }
    }    
}