using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakScheduleCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteKathavachakScheduleCommand(int id) => Id = id;
    }
}
