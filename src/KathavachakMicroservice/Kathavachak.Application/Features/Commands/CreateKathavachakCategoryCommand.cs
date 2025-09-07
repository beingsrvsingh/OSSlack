using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakCategoryCommand : IRequest<Result>
    {
        public int KathavachakId { get; set; }
        public int CategoryId { get; set; }
    }

}
