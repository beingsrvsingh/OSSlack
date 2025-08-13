using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestExpertiseConfiguration : IEntityTypeConfiguration<PriestExpertise>
    {
        public void Configure(EntityTypeBuilder<PriestExpertise> builder)
        {
            builder.ToTable("priest_expertise");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.PriestId).HasColumnName("priest_id").IsRequired();
            builder.Property(e => e.ExpertiseArea).HasColumnName("expertise_area").IsRequired();

            builder.HasOne(e => e.Priest)
                   .WithMany(p => p.PriestExpertise)
                   .HasForeignKey(e => e.PriestId);
        }
    }

}