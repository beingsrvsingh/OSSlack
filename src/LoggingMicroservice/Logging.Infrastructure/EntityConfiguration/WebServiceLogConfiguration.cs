using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.EntityConfiguration
{
    public class WebServiceLogConfiguration : BaseLogEntityConfiguration<WebServiceLog>
    {
        public override void Configure(EntityTypeBuilder<WebServiceLog> builder)
        {
            base.Configure(builder);
            builder.ToTable("web_service_logs");

            builder.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
            builder.Property(x => x.Endpoint).HasColumnName("endpoint").HasMaxLength(200);
            builder.Property(x => x.HttpMethod).HasColumnName("http_method").HasMaxLength(10);
            builder.Property(x => x.HttpStatusCode).HasColumnName("http_status_code");
            builder.Property(x => x.Referrer).HasColumnName("referrer").HasMaxLength(200);
            builder.Property(x => x.RequestBody).HasColumnName("request_body").HasColumnType("longtext");
            builder.Property(x => x.ResponseBody).HasColumnName("response_body").HasColumnType("longtext");
            builder.Property(x => x.UserAgent).HasColumnName("user_agent").HasMaxLength(300);
            builder.Property(x => x.ResponseTimeMs).HasColumnName("response_time_ms");
            builder.Property(x => x.ExceptionDetails).HasColumnName("exception_details").HasColumnType("longtext");

            builder.HasIndex(x => x.Endpoint).HasDatabaseName("idx_web_endpoint");
            builder.HasIndex(x => x.HttpStatusCode).HasDatabaseName("idx_web_http_status");

            builder.HasIndex(x => new { x.Endpoint, x.HttpStatusCode })
                   .HasDatabaseName("idx_endpoint_status");

        }
    }

}