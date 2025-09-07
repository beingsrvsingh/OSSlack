using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakTopicCommand : IRequest<Result>
    {
        public int KathavachakId { get; set; }
        public string TopicName { get; set; } = null!;
    }

}
