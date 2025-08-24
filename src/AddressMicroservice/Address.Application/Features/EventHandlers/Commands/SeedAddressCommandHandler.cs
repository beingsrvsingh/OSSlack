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
                    Uid = Guid.NewGuid(),
                    Name = "Saurav Sinha",
                    Label = "Home",
                    AddressLine1 = "Gate No.7 7005 - A FirstFloor",
                    AddressLine2 = "Kailash Colony",
                    City = "New Delhi",
                    State = "Delhi",
                    Country = "India",
                    Pincode = "110066",
                    Landmark = "Near Main Market",
                    PhoneNumber = "981806003"
                },
                new AddressEntity
                {
                    Uid = Guid.NewGuid(),
                    Name = "Jaksh Sinha",
                    Label = "Work",
                    AddressLine1 = "Gate No.9007 - A Fourth",
                    AddressLine2 = "East of Kailash",
                    City = "Lajpat Nagar",
                    State = "Delhi",
                    Pincode = "110055",
                    Landmark = "Near Metro Station",
                    PhoneNumber = "801806995"
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
                _logger.LogWarning("Seeding address failed.");
                return Result.Failure(new FailureResponse("SeedingFailed", "Failed to seed address."));
            }

            return Result.Success("Address seeded successfully.");
        }
    }
}