using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class StateQueryHandler : IRequestHandler<GetAllStateQuery, IResult>
    {
        public StateQueryHandler()
        {
            
        }
        public Task<IResult> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
