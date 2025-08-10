using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("service_categories");

            builder.HasKey(sc => sc.Id);

            builder.Property(sc => sc.Id)
                   .HasColumnName("id");

            builder.Property(sc => sc.ServicePackageId)
                   .HasColumnName("service_package_id")
                   .IsRequired();

            builder.Property(sc => sc.Name)
                   .HasColumnName("name")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(sc => sc.Description)
                   .HasColumnName("description")
                   .IsRequired(false);

            builder.Property(sc => sc.AdditionalPrice)
                   .HasColumnName("additional_price")
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Property(sc => sc.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(true)
                   .IsRequired();

            builder.HasIndex(sc => sc.Name)
                   .HasDatabaseName("ix_service_categories_name");

            builder.HasOne(sc => sc.ServicePackage)
                   .WithMany(sp => sp.ServiceCategories)
                   .HasForeignKey(sc => sc.ServicePackageId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}