using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakLanguageCommand : IRequest<Result>
    {
        public int KathavachakId { get; set; }
        public int LanguageId { get; set; }
    }

}
