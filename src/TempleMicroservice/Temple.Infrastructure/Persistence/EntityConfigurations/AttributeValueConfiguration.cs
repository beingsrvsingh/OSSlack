using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.ToTable("attribute_value");

            builder.HasKey(av => av.Id);

            builder.Property(av => av.Id)
                .HasColumnName("id");

            builder.Property(av => av.ExpertiseId)
                .IsRequired()
                .HasColumnName("expertise_id");

            builder.Property(av => av.CatalogAttributeId)
                .IsRequired()
                .HasColumnName("catalog_attribute_id");

            builder.Property(av => av.CatalogAttributeValueId)
                .IsRequired()
                .HasColumnName("catalog_attribute_value_id");

            builder.Property(av => av.Value)
                .IsRequired()
                .HasColumnName("value");

            builder.Property(av => av.AttributeKey)
                .HasColumnName("attribute_key");

            builder.Property(av => av.AttributeLabel)
                .HasColumnName("attribute_label");

            builder.Property(av => av.AttributeDataTypeId)
                .HasColumnName("attribute_data_type_id");

            builder.Property(av => av.CatalogAttributeGroupId)
                .HasColumnName("catalog_attribute_group_id");

            builder.Property(av => av.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            // Foreign key relationship
            builder.HasOne(aav => aav.TempleExpertise)
                  .WithMany(a => a.AttributeValues)
                  .HasForeignKey(aav => aav.ExpertiseId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(aav => new { aav.ExpertiseId, aav.CatalogAttributeId })
                   .HasDatabaseName("ix_temple_attribute_values_temple_catalogattribute");
        }
    }

}
