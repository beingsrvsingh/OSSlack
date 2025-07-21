using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecretManagement.Domain.Entities;

namespace SecretManagement.Infrastructure.Persistence.EntityConfigurations;

public class ApiSecretConfiguration : IEntityTypeConfiguration<ApiSecret>
{
    public void Configure(EntityTypeBuilder<ApiSecret> builder)
    {
        builder.ToTable("api_secrets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ApiKey)
        .HasColumnName("api_key")
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(x => x.ApiKey)
            .IsUnique();

        builder.Property(x => x.IsActive)
        .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.Property(x => x.LastUsedAt)
            .HasColumnName("last_used_at")
            .IsRequired(false);

        builder.Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired(false);

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
        .IsRequired()
            .HasMaxLength(450)  // assuming ASP.NET Identity default string length for UserId
            .IsRequired(false);
    }
}
