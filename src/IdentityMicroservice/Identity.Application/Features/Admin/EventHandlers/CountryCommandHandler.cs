using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class CountryCommandHandler : IRequestHandler<CountryCommand, Result>
    {
        private readonly ILoggerService<CountryCommandHandler> _logger;
        private readonly IAdminService service;

        public CountryCommandHandler(ILoggerService<CountryCommandHandler> logger, IAdminService service)
        {
            this._logger = logger;
            this.service = service;
        }
        public async Task<Result> Handle(CountryCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }
            
            var success = await service.AddCountryAsync(request);

            if (!success)
            {
                return Result.Failure(new FailureResponse("CountryInsertFailed", "Failed to add the country."));
            }

            return Result.Success("Country added successfully.");
        }
    }
}
