using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakTimeSlotCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DeleteKathavachakTimeSlotCommand(int id) => Id = id;
    }
}
