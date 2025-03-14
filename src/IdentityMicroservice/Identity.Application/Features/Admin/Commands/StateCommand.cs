using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class StateCommand : IRequest<Result>
    {
        public int CountryMasterId { get; set; }

        public string Name { get; set; } = null!;
    }
}
