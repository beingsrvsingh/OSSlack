using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleAartiLocalizedTextConfiguration : IEntityTypeConfiguration<TempleAartiLocalizedText>
    {
        public void Configure(EntityTypeBuilder<TempleAartiLocalizedText> builder)
        {
            builder.ToTable("TempleAartiLocalizedText");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.LanguageCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(t => t.LocalizedName)
                .HasMaxLength(150);

            builder.Property(t => t.LocalizedDescription);

            builder.HasOne(t => t.TempleAarti)
                .WithMany(a => a.Localizations)
                .HasForeignKey(t => t.TempleAartiId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}