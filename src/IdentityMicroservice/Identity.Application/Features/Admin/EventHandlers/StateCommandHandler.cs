using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class StateCommandHandler : IRequestHandler<StateCommand, Result>
    {
        private readonly ILoggerService<StateCommandHandler> _logger;
        private readonly IAdminService service;

        public StateCommandHandler(ILoggerService<StateCommandHandler> logger, IAdminService service)
        {
            this._logger = logger;
            this.service = service;
        }

        public async Task<Result> Handle(StateCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            var success = await service.AddStateAsync(request);

            if (!success)
            {
                return Result.Failure(new FailureResponse("CityInsertFailed", "Failed to add the city."));
            }

            return Result.Success("City added successfully.");
        }
    }
}
