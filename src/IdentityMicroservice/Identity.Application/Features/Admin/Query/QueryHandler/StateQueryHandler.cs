using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class StateQueryHandler : IRequestHandler<GetAllStateQuery, Result>
    {
        public StateQueryHandler()
        {
            
        }
        public Task<Result> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
