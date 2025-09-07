using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakLanguageConfiguration : IEntityTypeConfiguration<KathavachakLanguage>
    {
        public void Configure(EntityTypeBuilder<KathavachakLanguage> builder)
        {
            builder.ToTable("kathavachak_language");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .HasColumnName("id");

            builder.Property(l => l.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(l => l.LanguageCode)
                .HasColumnName("language_code")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasOne(l => l.Kathavachak)
                .WithMany(k => k.Languages)
                .HasForeignKey(l => l.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
