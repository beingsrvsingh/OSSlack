using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace PriestMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.ToTable("priest_attribute_value");

            builder.HasKey(aav => aav.Id);

            builder.Property(aav => aav.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(aav => aav.ExpertiseId)
                   .HasColumnName("expertise_id")
                   .IsRequired();

            builder.Property(aav => aav.PriestId)
                   .HasColumnName("priest_id");

            builder.Property(aav => aav.CatalogAttributeId)
                   .HasColumnName("catalog_attribute_id")
                   .IsRequired();

            builder.Property(aav => aav.CatalogAttributeValueId)
                   .HasColumnName("catalog_attribute_value_id")
                   .IsRequired();

            builder.Property(aav => aav.Value)
                   .HasColumnName("value")
                   .IsRequired();

            builder.Property(aav => aav.AttributeKey)
                   .HasColumnName("attribute_key")
                   .HasMaxLength(200);

            builder.Property(aav => aav.AttributeLabel)
                   .HasColumnName("attribute_label")
                   .HasMaxLength(200);

            builder.Property(aav => aav.AttributeDataTypeId)
                   .HasColumnName("attribute_data_type_id");

            builder.Property(aav => aav.CatalogAttributeGroupId)
                   .HasColumnName("catalog_attribute_group_id");

            builder.Property(aav => aav.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                   .IsRequired();

            builder.HasOne(pav => pav.PriestMaster)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(pav => pav.PriestId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(aav => aav.PriestExpertise)
                   .WithMany(a => a.AttributeValues)
                   .HasForeignKey(aav => aav.ExpertiseId)
                   .OnDelete(DeleteBehavior.SetNull);

            // Optional: Add indexes if necessary, e.g. on AstrologerId + CatalogAttributeId
            builder.HasIndex(aav => new { aav.ExpertiseId, aav.CatalogAttributeId })
                   .HasDatabaseName("ix_priest_attribute_values_priest_catalogattribute");
        }
    }

}
