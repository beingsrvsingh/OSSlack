using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Features.Commands;
using Review.Domain.Entities;
using System.Reflection;
using Utilities.Cryptography;

namespace Review.Application.Config
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<ReviewCommand, Review.Domain.Entities.Reviews>
                .NewConfig()
                .Map(src => src.ProductId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ProductId)))
                .Map(src => src.CreatedDate,
                dest => DateTime.Now);

            TypeAdapterConfig<ReviewReportDetailCommand, ReviewDetail>
                .NewConfig()
                .Map(src => src.ReviewId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ReviewId)))
                .Map(src => src.ProductId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ProductId)))
                .Map(src => src.ReviewReportLookUpId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ReportId)))
                .Map(src => src.CreatedDate,
                dest => DateTime.Now);

            TypeAdapterConfig<ReviewDetailHelpFulCommand, ReviewDetail>
                .NewConfig()
                .Map(src => src.ReviewId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ReviewId)))
                .Map(src => src.ProductId,
                dest => Int32.Parse(Cryptography.DecryptString(dest.ProductId)))
                .Map(src => src.CreatedDate,
                dest => DateTime.Now);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}