using MediatR;
using Priest.Domain.Entities.Enums;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreateConsultationModeCommand : IRequest<Result>
    {
        public int ExpertieseId { get; set; }
        public int ConsultationModeMasterId { get; set; }
        public ConsultationModeType Mode { get; set; }
    }
}
