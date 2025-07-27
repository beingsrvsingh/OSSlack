using Identity.Application.Features.Admin.Commands;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Mapster;
using MediatR;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.EventHandlers
{
    public class SeedLocationCommandHandler : IRequestHandler<SeedLocationCommand, Result>
    {
        private readonly ILoggerService<SeedLocationCommandHandler> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IAdminService service;
        private readonly IExcelService excelService;
        private readonly IEnvironmentService environmentService;

        public SeedLocationCommandHandler(ILoggerService<SeedLocationCommandHandler> logger,
        IUnitOfWork unitOfWork, IAdminService service, IExcelService excelService, IEnvironmentService environmentService)
        {
            this._logger = logger;
            this.unitOfWork = unitOfWork;
            this.service = service;
            this.excelService = excelService;
            this.environmentService = environmentService;
        }

        public async Task<Result> Handle(SeedLocationCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // Seed Countries
                var countryPath =  environmentService.GetStaticPath("country_master.json");
                using FileStream json = File.OpenRead(countryPath);
                var countryCommands = JsonSerializerWrapper.Deserialize<List<CountryCommand>>(json);
                var countries = countryCommands.Adapt<List<CountryMaster>>();

                var countrySaved = await service.BulkInsertCountriesAsync(countries);

                if (!countrySaved)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return Result.Failure(new FailureResponse("CountryInsertFailed", "Failed to save country data."));
                }

                // Seed States + Cities from Excel
                var excelPath = environmentService.GetStaticPath("pincode.xlsx");
                var excelDTOs = excelService.ReadLocationExcel(excelPath);

                if (!excelDTOs.Any())
                    return Result.Failure(new FailureResponse("ExcelEmpty", "No data found in Excel file."));

                var india = countries.FirstOrDefault(c => c.Name.Equals("India", StringComparison.OrdinalIgnoreCase));
                if (india == null)
                    return Result.Failure(new FailureResponse("IndiaMissing", "India not found in seeded countries."));

                var ordered = excelDTOs
                    .OrderBy(x => x.StateName)
                    .ThenBy(x => x.DistrictName)
                    .ThenBy(x => x.Pincode)
                    .ThenBy(x => x.AreaName)
                    .ToList();

                var states = ordered.DistinctBy(x => x.StateName).Adapt<List<StateMaster>>();
                states.ForEach(s => s.CountryMasterId = india.Id);

                var stateSaved = await service.BulkInsertStatesAsync(states);

                if (!stateSaved)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return Result.Failure(new FailureResponse("StateInsertFailed", "Failed to save state data."));
                }

                ordered.ForEach(x =>
                {
                    x.StateMasterId = states.First(s => s.Name == x.StateName).Id;
                });

                var cities = ordered.Adapt<List<CityMaster>>();

                var citySaved = await service.BulkInsertCitiesAsync(cities);

                if (!citySaved)
                {
                    await unitOfWork.RollbackTransactionAsync();
                    return Result.Failure(new FailureResponse("CityInsertFailed", "Failed to save city data."));
                }

                await unitOfWork.CommitTransactionAsync();
                return Result.Success("Country, State, and City data seeded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during seeding location data.");
                await unitOfWork.RollbackTransactionAsync();
                return Result.Failure(new FailureResponse("UnhandledException", ex.Message));
            }
        }
    }

}