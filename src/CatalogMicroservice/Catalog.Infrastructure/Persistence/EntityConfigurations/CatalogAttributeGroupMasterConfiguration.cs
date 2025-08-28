
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
                .HasColumnName("group_key")
                .HasMaxLength(100);

            builder.Property(c => c.DisplayName)
                .IsRequired()
                .HasColumnName("display_name")
                .HasMaxLength(200);

            builder.Property(c => c.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(c => c.IsActive)
                .HasColumnName("is_Active")
                .HasDefaultValue(true);
        }
    }

}