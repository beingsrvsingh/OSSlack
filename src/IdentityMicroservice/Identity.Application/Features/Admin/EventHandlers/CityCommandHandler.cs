using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class CityCommandHandler : IRequestHandler<CityCommand, Result>
    {
        private readonly IAdminService service;
        private readonly ILoggerService<CityCommandHandler> _logger;

        public CityCommandHandler(ILoggerService<CityCommandHandler> logger, IAdminService service)
        {
            this.service = service;
            this._logger = logger;
        }

        public async Task<Result> Handle(CityCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            var success = await service.AddCityAsync(request);

            if (!success)
            {
                return Result.Failure(new FailureResponse("CityInsertFailed", "Failed to add the city."));
            }

            return Result.Success("City added successfully.");
        }
    }    
}