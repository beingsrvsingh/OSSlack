using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class ExpertiseEntityConfiguration : IEntityTypeConfiguration<Expertise>
    {
        public void Configure(EntityTypeBuilder<Expertise> builder)
        {
            builder.ToTable("expertises");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(e => e.Name)
                   .IsUnique();

            builder.HasMany(e => e.TempleExpertises)
                   .WithOne(ae => ae.Expertise)
                   .HasForeignKey(ae => ae.ExpertiseId)
                   .HasConstraintName("fk_astrologer_expertise_expertise_id")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}