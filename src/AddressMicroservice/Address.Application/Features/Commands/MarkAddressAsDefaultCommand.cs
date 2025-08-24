using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Commands
{
    public class MarkAddressAsDefaultCommand() : IRequest<Result>
    {
        public int AddressId { get; set; }
    }
}