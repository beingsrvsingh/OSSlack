using Address.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Commands
{
    public class CreateAddressCommand : IRequest<Result>
    {
        public CreateAddressDto Address { get; set; }

        public CreateAddressCommand(CreateAddressDto address)
        {
            Address = address;
        }
    }
}