using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerLanguageConfiguration : IEntityTypeConfiguration<AstrologerLanguage>
    {
        public void Configure(EntityTypeBuilder<AstrologerLanguage> builder)
        {
            builder.ToTable("astrologer_language");  // Set table name

            builder.HasKey(al => al.Id);  // Primary key

            // Configure foreign key relationship with AstrologerEntity
            builder.HasOne(al => al.Astrologer)
                   .WithMany(a => a.AstrologerLanguages)
                   .HasForeignKey(al => al.AstrologerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Configure foreign key relationship with LanguageMaster
            builder.HasOne(al => al.Language)
                   .WithMany(l => l.AstrologerLanguages)
                   .HasForeignKey(al => al.LanguageId)
                   .OnDelete(DeleteBehavior.Cascade);

            // If you want, you can add indexes on AstrologerId and LanguageId
            builder.HasIndex(al => new { al.AstrologerId, al.LanguageId }).IsUnique();
        }
    }
}