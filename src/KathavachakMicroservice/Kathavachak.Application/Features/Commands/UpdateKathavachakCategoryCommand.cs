using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakCategoryCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }

}
