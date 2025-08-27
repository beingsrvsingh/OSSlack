
namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    using Catalog.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CatalogAttributeGroupMasterConfiguration : IEntityTypeConfiguration<CatalogAttributeGroupMaster>
    {
        public void Configure(EntityTypeBuilder<CatalogAttributeGroupMaster> builder)
        {
            builder.ToTable("attribute_group");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.GroupKey)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.DisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.SortOrder)
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .HasDefaultValue(true);
        }
    }

}