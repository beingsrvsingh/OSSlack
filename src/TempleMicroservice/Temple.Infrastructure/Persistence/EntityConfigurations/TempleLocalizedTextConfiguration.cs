using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleLocalizedTextConfiguration : IEntityTypeConfiguration<TempleLocalizedText>
    {
        public void Configure(EntityTypeBuilder<TempleLocalizedText> builder)
        {
            builder.ToTable("TempleLocalizedText");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.LanguageCode)
                   .IsRequired()
                   .HasMaxLength(5);

            builder.Property(t => t.LocalizedName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.LocalizedDescription)
                   .HasMaxLength(1000);

            builder.HasOne(t => t.Temple)
                   .WithMany(tm => tm.Localizations)
                   .HasForeignKey(t => t.TempleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}