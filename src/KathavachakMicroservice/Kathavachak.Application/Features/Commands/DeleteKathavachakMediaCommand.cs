using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakMediaCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteKathavachakMediaCommand(int id)
        {
            Id = id;
        }
    }

}
