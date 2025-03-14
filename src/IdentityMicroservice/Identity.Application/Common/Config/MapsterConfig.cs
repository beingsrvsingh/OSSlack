using Identity.Application.Contracts;
using Identity.Application.Features.Admin.Commands;
using Identity.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Identity.Application.Common.Config
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<CountryCommand, CountryMaster>
                .NewConfig()
                .Map(src => src.AlphaTwoCode,
                dest => dest.Code)
                .Map(src => src.DialCode,
                dest => dest.Dial_Code)
                .Map(src => src.ImageUri,
                dest => dest.Image);

            TypeAdapterConfig<ExcelDTO, StateMaster>
                .NewConfig()
                .Map(src => src.Name,
                dest => dest.StateName);

            TypeAdapterConfig<ExcelDTO, CityMaster>
                .NewConfig()
                .Map(src => src.StateMasterId,
                dest => dest.StateMasterId);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}