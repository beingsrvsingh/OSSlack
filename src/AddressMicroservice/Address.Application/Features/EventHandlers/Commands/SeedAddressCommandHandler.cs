using Address.Application.Features.Commands;
using Address.Application.Service;
using Address.Domain.Entities;
using Address.Domain.Enums;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Address.Application.Features.EventHandlers.Commands
{
    public class SeedAddressCommandHandler : IRequestHandler<SeedAddressCommand, Result>
    {
        private readonly ILoggerService<SeedAddressCommandHandler> _logger;
        private readonly ISeedService seedService;

        public SeedAddressCommandHandler(ILoggerService<SeedAddressCommandHandler> logger, ISeedService seedService)
        {
            this._logger = logger;
            this.seedService = seedService;
        }

        public async Task<Result> Handle(SeedAddressCommand request, CancellationToken cancellationToken)
        {
            List<AddressEntity> addressEntities = new List<AddressEntity>()
            {
                new AddressEntity
                {
                    Id = 1,
                    Uid = Guid.NewGuid(),
                    OwnerId = 1001,
                    OwnerType = AddressOwnerType.User,
                    Name = "Saurav Sinha",
                    Label = "Home",
                    AddressLine1 = "Gate No.7 7005 - A FirstFloor",
                    AddressLine2 = "Kailash Colony",
                    City = "New Delhi",
                    State = "Delhi",
                    Country = "India",
                    Pincode = "110066",
                    Landmark = "Near Main Market",
                    PhoneNumber = "981806003",
                    AddressTypeId = 1, // Home
                    IsDefault = true,
                    IsActive = true,
                    Latitude = 28.5561,
                    Longitude = 77.2436,
                    TimeZone = "Asia/Kolkata",
                    CreatedBy = 1,
                    UpdatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new AddressEntity
                {
                    Id = 2,
                    Uid = Guid.NewGuid(),
                    OwnerId = 1002,
                    OwnerType = AddressOwnerType.User,
                    Name = "Jaksh Sinha",
                    Label = "Work",
                    AddressLine1 = "Gate No.9007 - A Fourth",
                    AddressLine2 = "East of Kailash",
                    City = "Lajpat Nagar",
                    State = "Delhi",
                    Country = "India",
                    Pincode = "110055",
                    Landmark = "Near Metro Station",
                    PhoneNumber = "801806995",
                    AddressTypeId = 2, // Work
                    IsDefault = false,
                    IsActive = true,
                    Latitude = 28.5671,
                    Longitude = 77.2612,
                    TimeZone = "Asia/Kolkata",
                    CreatedBy = 2,
                    UpdatedBy = 2,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "User", Description = "Represents a general user address" },
                new AddressType { Id = 2, Name = "Partner", Description = "Address associated with a business partner" },
                new AddressType { Id = 3, Name = "Temple", Description = "Address of a temple location" },
                new AddressType { Id = 4, Name = "Priest", Description = "Address associated with a priest" },
                new AddressType { Id = 5, Name = "Astrologer", Description = "Address associated with an astrologer" },
                new AddressType { Id = 6, Name = "Vendor", Description = "Vendor's registered address" },
                new AddressType { Id = 7, Name = "DeliveryPartner", Description = "Address used by delivery personnel or partners" },
                new AddressType { Id = 8, Name = "Office", Description = "Office or workplace address" },
                new AddressType { Id = 9, Name = "Home", Description = "Personal or residential address" },
                new AddressType { Id = 10, Name = "Other", Description = "Any other type of address" }
            };


            var success = await seedService.SeedAddressAsync(addressEntities, addressTypes);

            if (!success)
            {
                _logger.LogWarning("Seeding astrologer experties failed.");
                return Result.Failure(new FailureResponse("SeedingFailed", "Failed to seed astrologer experties."));
            }

            return Result.Success("Astrologer experties seeded successfully.");
        }
    }
}