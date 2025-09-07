using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakScheduleCommand : IRequest<Result>
    {
        public int KathavachakId { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }

}
