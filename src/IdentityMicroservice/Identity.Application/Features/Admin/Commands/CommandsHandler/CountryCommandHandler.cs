using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shared.Utilities;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class CountryCommandHandler : IRequestHandler<CountryCommand, Result>
    {
        private readonly IAdminService service;
        private readonly IWebHostEnvironment env;

        public CountryCommandHandler(IAdminService service, IWebHostEnvironment env)
        {
            this.service = service;
            this.env = env;
        }
        public Task<Result> Handle(CountryCommand command, CancellationToken cancellationToken)
        {
            var path = Path.Combine(this.env.ContentRootPath, Constants.STATIC_FILE_PATH, "country_master.json");

            using FileStream json = File.OpenRead(path);
            List<CountryCommand>? countryCommands = JsonSerializerWrapper.Deserialize<List<CountryCommand>>(json);

            var request = countryCommands.Adapt<List<CountryMaster>>();

            service.AddCountryRange(request);

            return Task.FromResult(Result.Success());
        }
    }
}
