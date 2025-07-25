using Identity.Application.Contracts;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using Shared.Utilities.Response;
using Shared.Utilities;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class CityCommandHandler : IRequestHandler<CityCommand, Result>
    {
        private readonly IAdminService service;
        private readonly IWebHostEnvironment env;

        public CityCommandHandler(IAdminService service, IWebHostEnvironment env)
        {
            this.service = service;
            this.env = env;
        }

        public async Task<Result> Handle(CityCommand command, CancellationToken cancellationToken)
        {

            var path = Path.Combine(this.env.ContentRootPath, Constants.STATIC_FILE_PATH, "pincode.xlsx");

            /*
            using FileStream json = File.OpenRead(path);
            var root = await JsonSerializer.DeserializeAsync<CityCommand>(json, Helper._options);
            */

            List<ExcelDTO> excelDTOs = new List<ExcelDTO>();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;  //get Column Count
                int rowCount = worksheet.Dimension.End.Row;

                int rowIndex = 1;
                for (int row = 2; row <= rowCount; row++)
                {
                    ExcelDTO excelDto = new();
                    for (int col = 1; col <= colCount; col++)
                    {
                        if (worksheet.Cells[rowIndex, col].Value.ToString() == "Pincode")
                        {
                            excelDto.Pincode = Convert.ToInt32(worksheet.Cells[row, col].Value);
                        }
                        if (worksheet.Cells[rowIndex, col].Value.ToString() == "District")
                        {
                            excelDto.DistrictName = worksheet.Cells[row, col].Value?.ToString()!.Trim()!;
                        }
                        if (worksheet.Cells[rowIndex, col].Value.ToString() == "OfficeName")
                        {
                            excelDto.AreaName = worksheet.Cells[row, col].Value?.ToString()!.Trim()!;
                        }
                        if (worksheet.Cells[rowIndex, col].Value.ToString() == "StateName")
                        {
                            excelDto.StateName = worksheet.Cells[row, col].Value?.ToString()!.Trim()!;
                        }
                    }
                    excelDTOs.Add(excelDto);
                }

            }

            var countries = await service.GetCountryByNameAsync("India");

            var result = excelDTOs.OrderBy(st => st.StateName).ThenBy(d => d.DistrictName).ThenBy(pin => pin.Pincode).ThenBy(area => area.AreaName).ToList();

            var states = result.DistinctBy(st => st.StateName);

            var statesMaster = states.Adapt<List<StateMaster>>();

            statesMaster.ForEach(state => state.CountryMasterId = countries!.Id);

            await service.AddStateRange(statesMaster);

            result.ForEach(r => r.StateMasterId = (statesMaster.Where(st => st.Name == r.StateName).FirstOrDefault()!.Id));

            var cities = result.Adapt<List<CityMaster>>();

            await service.AddCityRange(cities);
            
            return Result.Success();
        }
    }    
}