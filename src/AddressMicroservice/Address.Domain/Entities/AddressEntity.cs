
using System.ComponentModel.DataAnnotations;
using Address.Domain.Enums;

namespace Address.Domain.Entities
{
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }

        public Guid Uid { get; set; } = Guid.NewGuid();

        [Required]
        public int OwnerId { get; set; }

        [Required]
        public AddressOwnerType OwnerType { get; set; } = AddressOwnerType.User;

        public string? Name { get; set; }

        public string? Label { get; set; }

        [Required]
        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string State { get; set; } = null!;

        [Required]
        public string Country { get; set; } = "India";

        [Required]
        public string Pincode { get; set; } = null!;

        public string? Landmark { get; set; }

        public string? PhoneNumber { get; set; }

        // Foreign key to AddressType
        public int AddressTypeId { get; set; }
        public AddressType AddressType { get; set; } = null!; 

        public bool IsDefault { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string? TimeZone { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }


}