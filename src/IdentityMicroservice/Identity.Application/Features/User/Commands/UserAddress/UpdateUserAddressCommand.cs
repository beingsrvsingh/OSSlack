using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserAddress
{
    public class UpdateUserAddressCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Address1 { get; set; } = null!;
        public string Address2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
    }
}
