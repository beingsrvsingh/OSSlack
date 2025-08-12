using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TemplePoojaLocalizedTextConfiguration : IEntityTypeConfiguration<TemplePoojaLocalizedText>
    {
        public void Configure(EntityTypeBuilder<TemplePoojaLocalizedText> builder)
        {
            builder.ToTable("TemplePoojaLocalizedText");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.LanguageCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(t => t.LocalizedName)
                .HasMaxLength(150);

            builder.Property(t => t.LocalizedDescription);

            builder.HasOne(t => t.TemplePooja)
                .WithMany(p => p.Localizations)
                .HasForeignKey(t => t.TemplePoojaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}