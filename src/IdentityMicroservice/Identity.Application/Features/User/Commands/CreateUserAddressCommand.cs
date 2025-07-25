    using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Features.User.Commands
{
    public class CreateUserAddressCommand : IRequest<Result>
    {
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
