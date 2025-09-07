using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakMasterCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteKathavachakMasterCommand(int id)
        {
            Id = id;
        }
    }

}
