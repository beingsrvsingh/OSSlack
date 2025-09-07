using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakLanguageCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
    }

}
