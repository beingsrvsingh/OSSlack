using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class CatalogAttributeIconConfiguration : IEntityTypeConfiguration<CatalogAttributeIcon>
    {
        public void Configure(EntityTypeBuilder<CatalogAttributeIcon> builder)
        {
            builder.ToTable("catalog_attribute_icon");

            builder.HasKey(cai => cai.Id);
            builder.Property(cai => cai.Id)
                .HasColumnName("id");

            builder.Property(cai => cai.IconName)
                .HasMaxLength(100)
                .HasColumnName("icon_name")
                .IsRequired(false);

            builder.Property(cai => cai.IconCodePoint)
                .HasColumnName("icon_code_point")
                .IsRequired(false);

            builder.Property(cai => cai.IconFontFamily)
                .HasMaxLength(100)
                .HasColumnName("icon_font_family")
                .IsRequired(false);
        }
    }

}