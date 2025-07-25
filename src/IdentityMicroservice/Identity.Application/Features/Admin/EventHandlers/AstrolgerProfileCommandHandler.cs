using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class AstrolgerProfileCommandHandler : IRequestHandler<AstrolgerProfileCommand, Result>
    {
        public AstrolgerProfileCommandHandler()
        {

        }

        public Task<Result> Handle(AstrolgerProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
