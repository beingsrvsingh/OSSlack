using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakLanguageCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteKathavachakLanguageCommand(int id)
        {
            Id = id;
        }
    }

}
