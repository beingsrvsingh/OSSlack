using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaLocalizedTextConfiguration : IEntityTypeConfiguration<PoojaLocalizedText>
    {
        public void Configure(EntityTypeBuilder<PoojaLocalizedText> builder)
        {
            builder.ToTable("pooja_localized_text");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasDefaultValue("en");

            builder.Property(l => l.LocalizedName)
                .HasMaxLength(150);

            builder.HasOne(l => l.PoojaMaster)
                .WithMany(p => p.Localizations)
                .HasForeignKey(l => l.PoojaMasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}