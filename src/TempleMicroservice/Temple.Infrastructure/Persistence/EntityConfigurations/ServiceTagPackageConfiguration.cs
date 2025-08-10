using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class ServiceTagPackageConfiguration : IEntityTypeConfiguration<ServiceTagPackage>
    {
        public void Configure(EntityTypeBuilder<ServiceTagPackage> builder)
        {
            builder.HasKey(st => st.Id);

            builder.HasOne(st => st.ServicePackage)
                .WithMany(sp => sp.ServiceTagPackages)
                .HasForeignKey(st => st.ServicePackageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(st => st.Tag)
                .WithMany(t => t.serviceTags)
                .HasForeignKey(st => st.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(st => new { st.ServicePackageId, st.TagId })
                .IsUnique(); // Optional: Prevent duplicate tag-package links
        }
    }
}