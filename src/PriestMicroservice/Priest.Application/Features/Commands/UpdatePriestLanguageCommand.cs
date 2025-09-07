using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class UpdatePriestLanguageCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
    }
}
