using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaKitLocalizedTextConfiguration : IEntityTypeConfiguration<PoojaKitLocalizedText>
    {
        public void Configure(EntityTypeBuilder<PoojaKitLocalizedText> builder)
        {
            builder.ToTable("pooja_kit_localized_text");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("id");

            builder.Property(l => l.PoojaKitId)
                .IsRequired()
                .HasColumnName("pooja_kit_id");

            builder.Property(l => l.LanguageCode)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("language_code");

            builder.Property(l => l.LocalizedName)
                .HasMaxLength(150)
                .HasColumnName("localized_name");

            builder.Property(l => l.LocalizedDescription)
                .HasColumnName("localized_description");

            builder.HasOne(l => l.PoojaKit)
                .WithMany(pk => pk.Localizations)
                .HasForeignKey(l => l.PoojaKitId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}