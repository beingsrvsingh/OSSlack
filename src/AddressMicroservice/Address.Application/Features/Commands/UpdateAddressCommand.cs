using Address.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features
{
    public class UpdateAddressCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public required AddressDto Address { get; set; }
    }
}