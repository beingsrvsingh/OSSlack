using MediatR;
using Pooja.Application.Features.Commands;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Commands
{
    public class UpdatePoojaCommandHandler : IRequestHandler<UpdatePoojaCommand, Result>
    {
        private readonly IPoojaService _service;
        private readonly ILoggerService<UpdatePoojaCommandHandler> _logger;

        public UpdatePoojaCommandHandler(IPoojaService service, ILoggerService<UpdatePoojaCommandHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdatePoojaCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var pooja = await _service.GetPoojaByIdAsync(request.Id);
                if (pooja == null)
                    return Result.Failure("Pooja not found.");

                pooja.Name = request.Name;
                pooja.Description = request.Description;
                pooja.IsHomeAvailable = request.IsHomeAvailable;
                pooja.BasePrice = request.Price;

                await _service.UpdatePoojaAsync(pooja);
                return Result.Success(pooja);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update pooja with id: {request.Id}");
                return Result.Failure("Unable to update pooja.");
            }
        }
    }

}
