using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Contracts
{
    public class DeleteAddressCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}