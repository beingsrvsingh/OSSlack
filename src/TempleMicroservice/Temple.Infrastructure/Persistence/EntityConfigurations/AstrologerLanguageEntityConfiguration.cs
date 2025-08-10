using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerLanguageEntityConfiguration : IEntityTypeConfiguration<AstrologerLanguage>
    {
        public void Configure(EntityTypeBuilder<AstrologerLanguage> builder)
        {
            builder.ToTable("astrologer_languages");

            builder.HasKey(al => new { al.AstrologerId, al.LanguageId });

            builder.HasOne(al => al.TempleMaster)
                   .WithMany(a => a.AstrologerLanguages)
                   .HasForeignKey(al => al.AstrologerId)
                   .HasConstraintName("fk_astrologer_language_astrologer_id");

            builder.HasOne(al => al.Language)
                   .WithMany(l => l.AstrologerLanguages)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_astrologer_language_language_id");
        }
    }

}