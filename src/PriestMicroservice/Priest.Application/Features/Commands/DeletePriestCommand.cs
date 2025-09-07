using MediatR;
using Shared.Utilities.Response;

namespace Priest.Application.Features.Commands
{
    public class DeletePriestCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

}
