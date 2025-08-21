
namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    using Catalog.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CatalogAttributeAllowedValueConfiguration : IEntityTypeConfiguration<CatalogAttributeAllowedValue>
    {
        public void Configure(EntityTypeBuilder<CatalogAttributeAllowedValue> builder)
        {
            builder.ToTable("catalog_attribute_allowed_value");

            builder.HasKey(av => av.Id);
            builder.Property(av => av.Id)
                .HasColumnName("id");                

            builder.Property(av => av.CatalogAttributeId)
                .IsRequired()
                .HasColumnName("catalog_attribute_id");

            builder.HasOne(av => av.CatalogAttribute)
                .WithMany(ca => ca.AllowedValues)
                .HasForeignKey(av => av.CatalogAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(av => av.Value)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("value");

            builder.Property(av => av.SortOrder)
                .HasColumnName("sort_order")
                .HasDefaultValue(0);

            builder.Property(av => av.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }

}