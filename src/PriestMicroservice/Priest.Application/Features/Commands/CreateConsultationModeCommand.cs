using MediatR;
using Priest.Domain.Entities.Enums;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreateConsultationModeCommand : IRequest<Result>
    {
        public int PriestId { get; set; }
        public ConsultationModeType Mode { get; set; }
    }
}
