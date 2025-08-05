using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
    {
        public void Configure(EntityTypeBuilder<ServicePackage> builder)
        {
            builder.ToTable("service_packages");

            builder.HasKey(sp => sp.Id);

            builder.Property(sp => sp.Id)
                   .HasColumnName("id");

            builder.Property(sp => sp.AstrologerId)
                   .HasColumnName("astrologer_id")
                   .IsRequired();

            builder.Property(sp => sp.Name)
                   .HasColumnName("name")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(sp => sp.Description)
                   .HasColumnName("description")
                   .IsRequired(false);

            builder.Property(sp => sp.Price)
                   .HasColumnName("price")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(sp => sp.Duration)
                   .HasColumnName("duration")
                   .IsRequired();

            builder.Property(sp => sp.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.HasIndex(sp => sp.AstrologerId)
                   .HasDatabaseName("ix_service_packages_astrologer_id");

            builder.HasOne(sp => sp.Astrologer)
                   .WithMany(a => a.ServicePackages)
                   .HasForeignKey(sp => sp.AstrologerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(sp => sp.ServiceCategories)
                   .WithOne(sc => sc.ServicePackage)
                   .HasForeignKey(sc => sc.ServicePackageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}