
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features;

public class DeleteUserAddressCommand : IRequest<Result>
{
    public required string AddressId { get; set; }
}