using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TemplePrasadLocalizedTextConfiguration : IEntityTypeConfiguration<TemplePrasadLocalizedText>
    {
        public void Configure(EntityTypeBuilder<TemplePrasadLocalizedText> builder)
        {
            builder.ToTable("TemplePrasadLocalizedText");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.LanguageCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(t => t.LocalizedName)
                .HasMaxLength(150);

            builder.Property(t => t.LocalizedDescription);

            builder.HasOne(t => t.TemplePrasad)
                .WithMany(p => p.Localizations)
                .HasForeignKey(t => t.TemplePrasadId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}