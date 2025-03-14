using MediatR;
using Shared.Utilities.Response;

namespace Partner.Application.Features.Commands.CommandsHandlers
{
    public class AstrolgerCommandHandler : IRequestHandler<AstrolgerCommand, Result>
    {
        public AstrolgerCommandHandler()
        {
            
        }

        public Task<Result> Handle(AstrolgerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
