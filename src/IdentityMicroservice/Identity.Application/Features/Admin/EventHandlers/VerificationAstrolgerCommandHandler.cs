using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class VerificationAstrolgerCommandHandler : IRequestHandler<VerificationAstrologerCommand, Result>
    {
        public VerificationAstrolgerCommandHandler()
        {
            
        }

        public Task<Result> Handle(VerificationAstrologerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
