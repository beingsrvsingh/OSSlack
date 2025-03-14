using Logging.Application.Features.Command;
using Logging.Domain.Entities;
using Logging.Domain.Service;
using Mapster;
using MediatR;

namespace Logging.Applicaton.Features.Handlers.Command
{
    public class LogCommandHandler : IRequestHandler<LogCommand>
    {
        private readonly ILogService logService;

        public LogCommandHandler(ILogService logService)
        {
            this.logService = logService;
        }
        public Task Handle(LogCommand request, CancellationToken cancellationToken)
        {
            var log = request.Adapt<Log>();
            logService.Add(log);
            return Task.CompletedTask;
        }
    }
}
