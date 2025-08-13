using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class ServicePackageConfiguration : IEntityTypeConfiguration<ServicePackage>
    {
        public void Configure(EntityTypeBuilder<ServicePackage> builder)
        {
            builder.ToTable("service_package");

            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id).HasColumnName("id");

            builder.Property(sp => sp.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(sp => sp.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(sp => sp.Description).HasColumnName("description").HasMaxLength(500);
            builder.Property(sp => sp.Price).HasColumnName("price").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(sp => sp.DurationInMinutes).HasColumnName("duration_in_minutes").IsRequired();

            builder.HasOne(sp => sp.Priest)
                   .WithMany(p => p.RitualServicePackages)
                   .HasForeignKey(sp => sp.PriestId);
        }
    }

}