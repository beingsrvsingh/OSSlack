using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<LanguageMaster>
    {
        public void Configure(EntityTypeBuilder<LanguageMaster> builder)
        {
            builder.ToTable("languages");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(l => l.Name)
                   .IsUnique();

            builder.HasMany(l => l.AstrologerLanguages)
                   .WithOne(al => al.Language)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_astrologer_language_language_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}