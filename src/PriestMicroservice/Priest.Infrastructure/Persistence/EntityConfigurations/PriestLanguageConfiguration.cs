using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestLanguageConfiguration : IEntityTypeConfiguration<PriestLanguage>
    {
        public void Configure(EntityTypeBuilder<PriestLanguage> builder)
        {
            builder.ToTable("priest_language");

            builder.HasKey(pl => pl.Id);
            builder.Property(pl => pl.Id).HasColumnName("id");

            builder.Property(pl => pl.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(pl => pl.Language).HasColumnName("language").HasMaxLength(100).IsRequired();

            builder.HasOne(pl => pl.Priest)
                   .WithMany(p => p.PriestLanguages)
                   .HasForeignKey(pl => pl.PriestId);
        }
    }

}