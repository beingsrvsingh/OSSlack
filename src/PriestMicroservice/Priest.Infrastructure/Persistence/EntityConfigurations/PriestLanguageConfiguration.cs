using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestLanguageConfiguration : IEntityTypeConfiguration<PriestLanguage>
    {
        public void Configure(EntityTypeBuilder<PriestLanguage> builder)
        {
            builder.ToTable("priest_language");

            builder.HasKey(al => al.Id);

            builder.Property(al => al.Id)
                   .HasColumnName("id");

            builder.Property(al => al.PriestId)
                   .HasColumnName("priest_id");

            builder.Property(al => al.LanguageId)
                   .HasColumnName("language_id");

            builder.Property(al => al.LanguageName)
                   .HasColumnName("language_name");

            builder.HasOne(al => al.Priest)
                   .WithMany(a => a.PriestLanguages)
                   .HasForeignKey(al => al.PriestId)
                   .HasConstraintName("fk_priest_language_priest_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(al => al.Language)
                   .WithMany(l => l.PriestLanguages)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_priest_language_language_id")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(al => new { al.PriestId, al.LanguageId })
                   .IsUnique()
                   .HasDatabaseName("ux_priest_language_priestid_languageid");
        }
    }

}