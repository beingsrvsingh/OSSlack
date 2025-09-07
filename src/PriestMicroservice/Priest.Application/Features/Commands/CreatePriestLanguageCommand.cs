using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class CreatePriestLanguageCommand : IRequest<Result>
    {
        public int PriestId { get; set; }
        public string Language { get; set; } = null!;
    }
}
