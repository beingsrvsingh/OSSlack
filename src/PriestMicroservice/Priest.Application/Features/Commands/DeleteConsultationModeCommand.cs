using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class DeleteConsultationModeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
