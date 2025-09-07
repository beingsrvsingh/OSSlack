using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class DeleteKathavachakTopicCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public DeleteKathavachakTopicCommand(int id)
        {
            Id = id;
        }
    }

}
