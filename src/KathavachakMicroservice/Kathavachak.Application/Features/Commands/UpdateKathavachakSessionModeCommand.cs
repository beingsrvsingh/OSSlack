using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakSessionModeCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

}
