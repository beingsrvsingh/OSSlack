using MediatR;
using Priest.Domain.Entities.Enums;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdateConsultationModeCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int ConsultationModeMasterId { get; set; }
        public ConsultationModeType Mode { get; set; }
    }
}
