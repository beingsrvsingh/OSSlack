using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class TagMasterConfiguration : IEntityTypeConfiguration<ServiceTagPackageMaster>
    {
        public void Configure(EntityTypeBuilder<ServiceTagPackageMaster> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.HasMany(t => t.serviceTags)
                .WithOne(st => st.Tag)
                .HasForeignKey(st => st.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}