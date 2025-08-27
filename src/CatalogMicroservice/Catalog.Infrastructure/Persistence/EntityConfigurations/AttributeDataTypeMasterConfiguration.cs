using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class AttributeDataTypeMasterConfiguration : IEntityTypeConfiguration<AttributeDataTypeMaster>
    {
        public void Configure(EntityTypeBuilder<AttributeDataTypeMaster> entity)
        {
            entity.ToTable("attribute_datatype");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();

            // One-to-many relationship
            entity.HasMany(e => e.CatalogAttributes)
                  .WithOne(ca => ca.AttributeDataType)
                  .HasForeignKey(ca => ca.AttributeDataTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}