using AstrologerMicroservice.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AstrologerMicroservice.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerExpertiseEntityConfiguration : IEntityTypeConfiguration<AstrologerExpertise>
    {
        public void Configure(EntityTypeBuilder<AstrologerExpertise> builder)
        {
            builder.ToTable("astrologer_expertises");

            builder.HasKey(ae => new { ae.AstrologerId, ae.ExpertiseId });

            builder.HasOne(ae => ae.Astrologer)
                   .WithMany(a => a.AstrologerExpertises)
                   .HasForeignKey(ae => ae.AstrologerId)
                   .HasConstraintName("fk_astrologer_expertise_astrologer_id");

            builder.HasOne(ae => ae.Expertise)
                   .WithMany(e => e.AstrologerExpertises)
                   .HasForeignKey(ae => ae.ExpertiseId)
                   .HasConstraintName("fk_astrologer_expertise_expertise_id");
        }
    }

}