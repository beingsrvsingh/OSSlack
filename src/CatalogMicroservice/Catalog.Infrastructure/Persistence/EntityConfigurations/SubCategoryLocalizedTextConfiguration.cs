using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class SubCategoryLocalizedTextConfiguration : IEntityTypeConfiguration<SubCategoryLocalizedText>
    {
        public void Configure(EntityTypeBuilder<SubCategoryLocalizedText> builder)
        {
            builder.ToTable("sub_category_localized_text");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("id");

            builder.Property(l => l.SubCategoryId)
                .IsRequired()
                .HasColumnName("sub_category_id");

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("language_code");

            builder.Property(l => l.LocalizedName)
                .HasMaxLength(150)
                .HasColumnName("localized_name");

            builder.Property(l => l.LocalizedDescription)
                .HasColumnName("localized_description");

            builder.HasOne(l => l.SubCategory)
                .WithMany(sc => sc.Localizations)
                .HasForeignKey(l => l.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}