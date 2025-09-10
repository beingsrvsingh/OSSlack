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

            builder.Property(l => l.LanguageId)
                .HasColumnName("language_id")
                .IsRequired();

            builder.Property(l => l.LanguageName)
                .HasColumnName("language_name")
                .IsRequired();

            builder.HasOne(l => l.Kathavachak)
                .WithMany(k => k.Languages)
                .HasForeignKey(l => l.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(al => al.Kathavachak)
                   .WithMany(a => a.Languages)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_kathavachak_language_kathavachak_id");

            builder.HasOne(al => al.Language)
                   .WithMany(l => l.KathavachakLanguages)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_kathavachak_language_language_id");
        }
    }

}
