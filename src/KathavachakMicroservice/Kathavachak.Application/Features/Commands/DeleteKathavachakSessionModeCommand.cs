using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakSessionModeCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteKathavachakSessionModeCommand(int id) => Id = id;
    }
}
