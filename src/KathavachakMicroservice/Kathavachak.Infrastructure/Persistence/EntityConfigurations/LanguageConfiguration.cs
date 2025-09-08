using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<LanguageMaster>
    {
        public void Configure(EntityTypeBuilder<LanguageMaster> builder)
        {
            builder.ToTable("languages");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .IsRequired()
                   .HasColumnName("language_name")
                   .HasMaxLength(100);

            builder.Property(l => l.Code)
                   .IsRequired()
                   .HasColumnName("language_code")
                   .HasMaxLength(10);

            builder.HasIndex(l => l.Name)
                   .IsUnique();

            builder.Property(l => l.DisplayOrder)
                   .HasColumnName("display_order");

            builder.HasMany(l => l.KathavachakLanguages)
                   .WithOne(al => al.Language)
                   .HasForeignKey(al => al.LanguageId)
                   .HasConstraintName("fk_kathavachak_language_language_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}