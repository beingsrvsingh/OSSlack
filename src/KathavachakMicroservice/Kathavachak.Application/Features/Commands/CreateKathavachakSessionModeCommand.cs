using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakSessionModeCommand : IRequest<Result>
    {
        public string Name { get; set; } = null!;
    }

}
