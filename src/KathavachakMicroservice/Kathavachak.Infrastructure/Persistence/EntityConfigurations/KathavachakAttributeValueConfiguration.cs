using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrologer.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakAttributeValueConfiguration : IEntityTypeConfiguration<KathavachakAttributeValue>
    {
        public void Configure(EntityTypeBuilder<KathavachakAttributeValue> builder)
        {
            builder.ToTable("kathavachak_attribute_value");

            builder.HasKey(aav => aav.Id);

            builder.Property(aav => aav.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(aav => aav.ExpertiseId)
                   .HasColumnName("expertise_id")
                   .IsRequired();

            builder.Property(aav => aav.KathavachakId)
                   .HasColumnName("kathavachak_id");

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

            builder.HasOne(pav => pav.KathavachakMaster)
                .WithMany(p => p.AttributeValues)
                .HasForeignKey(pav => pav.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(aav => aav.KathavachakExpertise)
                   .WithMany(a => a.KathavachakAttributeValues)
                   .HasForeignKey(aav => aav.ExpertiseId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(aav => new { aav.ExpertiseId, aav.CatalogAttributeId })
                   .HasDatabaseName("ix_kathavachak_attribute_values_kathavachak_catalogattribute");
        }
    }

}
