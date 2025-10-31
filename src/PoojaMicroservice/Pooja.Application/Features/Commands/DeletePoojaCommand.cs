using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Commands
{
    public class DeletePoojaCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

}
