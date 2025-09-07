using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class DeletePriestLanguageCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
