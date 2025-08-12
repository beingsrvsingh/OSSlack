using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleDonationLocalizedTextConfiguration : IEntityTypeConfiguration<TempleDonationLocalizedText>
{
    public void Configure(EntityTypeBuilder<TempleDonationLocalizedText> builder)
    {
        builder.ToTable("TempleDonationLocalizedText");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.LanguageCode)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(t => t.LocalizedName)
            .HasMaxLength(150);

        builder.Property(t => t.LocalizedDescription);

        builder.HasOne(t => t.TempleDonation)
            .WithMany(d => d.Localizations)
            .HasForeignKey(t => t.TempleDonationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

}