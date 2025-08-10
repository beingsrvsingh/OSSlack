using Temple.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
    public class TempleExpertiseEntityConfiguration : IEntityTypeConfiguration<TempleExpertise>
    {
        public void Configure(EntityTypeBuilder<TempleExpertise> builder)
        {
            builder.ToTable("astrologer_expertises");

            builder.HasKey(ae => new { ae.AstrologerId, ae.ExpertiseId });

            builder.HasOne(ae => ae.TempleMaster)
                   .WithMany(a => a.TempleExpertises)
                   .HasForeignKey(ae => ae.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id");

            builder.HasOne(ae => ae.Expertise)
                   .WithMany(e => e.TempleExpertises)
                   .HasForeignKey(ae => ae.ExpertiseId)
                   .HasConstraintName("fk_astrologer_expertise_expertise_id");
        }
    }

}