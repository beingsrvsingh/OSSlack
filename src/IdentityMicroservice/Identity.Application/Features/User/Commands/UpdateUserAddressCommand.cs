using System.ComponentModel.DataAnnotations;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserAddress
{
    public class UpdateUserAddressCommand : IRequest<Result>
    {   
        public required string Id { get; set; }
        public required string UserId { get; set; }
        [Required]
        public required string StreetAddress { get; set; }

        public string? ApartmentSuitUnitAddress { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string State { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required string ZipCode { get; set; }
    }
}
