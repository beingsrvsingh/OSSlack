using MediatR;
using Pooja.Application.Features.Commands;
using Pooja.Application.Services;
using Pooja.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Commands
{
    public class CreatePoojaCommandHandler : IRequestHandler<CreatePoojaCommand, Result>
    {
        private readonly IPoojaService _service;
        private readonly ILoggerService<CreatePoojaCommandHandler> _logger;

        public CreatePoojaCommandHandler(IPoojaService service, ILoggerService<CreatePoojaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(CreatePoojaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pooja = new PoojaMaster
                {
                    Name = request.Name,
                };

                await _service.AddPoojaAsync(pooja);
                return Result.Success(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create pooja.");
                return Result.Failure("Unable to create pooja.");
            }
        }
    }

}
