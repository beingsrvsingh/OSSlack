using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PriestMicroservice.Domain.Entities;

namespace PriestMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class LanguageMasterConfiguration : IEntityTypeConfiguration<LanguageMaster>
    {
        public void Configure(EntityTypeBuilder<LanguageMaster> builder)
        {
            builder.ToTable("language_master");

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

            builder.HasMany(l => l.PriestLanguages)
                    .WithOne(al => al.Language)
                    .HasForeignKey(al => al.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
