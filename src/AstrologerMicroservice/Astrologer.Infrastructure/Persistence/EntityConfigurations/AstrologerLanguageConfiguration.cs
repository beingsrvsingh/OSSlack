using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerLanguageConfiguration : IEntityTypeConfiguration<AstrologerLanguage>
    {
        public void Configure(EntityTypeBuilder<AstrologerLanguage> builder)
        {
            builder.ToTable("astrologer_language");  // snake_case table name

            builder.HasKey(al => al.Id);

            builder.Property(al => al.Id)
                   .HasColumnName("id");

            builder.Property(al => al.AstrologerId)
                   .HasColumnName("astrologer_id");

            builder.Property(al => al.LanguageId)
                   .HasColumnName("language_id");

            builder.Property(al => al.LanguageName)
                   .HasColumnName("language_name");

            builder.HasOne(al => al.Astrologer)
                   .WithMany(a => a.AstrologerLanguages)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_language_astrologer_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(al => al.Language)
                   .WithMany(l => l.AstrologerLanguages)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_astrologer_language_language_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(al => new { al.AstrologerId, al.LanguageId })
                   .IsUnique()
                   .HasDatabaseName("ux_astrologer_language_astrologerid_languageid");
        }

    }
}