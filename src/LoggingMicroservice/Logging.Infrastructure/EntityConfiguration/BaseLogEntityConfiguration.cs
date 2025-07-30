using Logging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Infrastructure.EntityConfiguration
{
    public abstract class BaseLogEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseLogEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Timestamp).HasColumnName("timestamp");

            builder.Property(x => x.LogLevel)
                .HasColumnName("log_level")
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Message)
                .HasColumnName("message")
                .IsRequired();

            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.SessionId).HasColumnName("session_id");
            builder.Property(x => x.DeviceInfo).HasColumnName("device_info");
            builder.Property(x => x.AppVersion).HasColumnName("app_version");
            builder.Property(x => x.ExtraData).HasColumnName("extra_data");

            builder.Property(x => x.ExceptionMessage).HasColumnName("exception_message");
            builder.Property(x => x.ExceptionType).HasColumnName("exception_type");
            builder.Property(x => x.StackTrace).HasColumnName("stack_trace");
            builder.Property(x => x.InnerExceptionMessage).HasColumnName("inner_exception_message");
            builder.Property(x => x.InnerExceptionType).HasColumnName("inner_exception_type");
            builder.Property(x => x.InnerStackTrace).HasColumnName("inner_stack_trace");

            builder.Property(x => x.ThreadId).HasColumnName("thread_id");
            builder.Property(x => x.ThreadName).HasColumnName("thread_name");

            builder.Property(x => x.Source).HasColumnName("source");
            builder.Property(x => x.MethodName).HasColumnName("method_name");
            builder.Property(x => x.LineNumber).HasColumnName("line_number");
            builder.Property(x => x.FileName).HasColumnName("file_name");

            builder.Property(x => x.CrashOccurred).HasColumnName("crash_occurred");
            builder.Property(x => x.Breadcrumbs).HasColumnName("breadcrumbs");
            builder.Property(x => x.MemoryUsageDetails).HasColumnName("memory_usage_details");
            builder.Property(x => x.CpuUsage).HasColumnName("cpu_usage");
            builder.Property(x => x.NetworkStatus).HasColumnName("network_status");

            builder.Property(x => x.Locale).HasColumnName("locale");
            builder.Property(x => x.AppState).HasColumnName("app_state");

            builder.HasIndex(x => x.Timestamp).HasDatabaseName("idx_log_timestamp");
            builder.HasIndex(x => x.LogLevel).HasDatabaseName("idx_log_log_level");
            builder.HasIndex(x => x.UserId).HasDatabaseName("idx_log_user_id");
            builder.HasIndex(x => x.SessionId).HasDatabaseName("idx_log_session_id");
            builder.HasIndex(x => new { x.LogLevel, x.Timestamp })
                   .HasDatabaseName("idx_log_level_timestamp");

        }
    }
}