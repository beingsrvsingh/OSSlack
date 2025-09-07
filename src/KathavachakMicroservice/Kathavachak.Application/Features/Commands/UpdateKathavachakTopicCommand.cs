using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakTopicCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
    }

}
