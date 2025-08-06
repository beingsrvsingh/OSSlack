using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class CategoryLocalizedTextConfiguration : IEntityTypeConfiguration<CategoryLocalizedText>
    {
        public void Configure(EntityTypeBuilder<CategoryLocalizedText> builder)
        {
            builder.ToTable("category_localized_text");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("id");

            builder.Property(l => l.CategoryId)
                .IsRequired()
                .HasColumnName("category_id");

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("language_code");

            builder.Property(l => l.LocalizedName)
                .HasMaxLength(150)
                .HasColumnName("localized_name");

            builder.Property(l => l.LocalizedDescription)
                .HasColumnName("localized_description");

            builder.HasOne(l => l.Category)
                .WithMany(c => c.Localizations)
                .HasForeignKey(l => l.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}