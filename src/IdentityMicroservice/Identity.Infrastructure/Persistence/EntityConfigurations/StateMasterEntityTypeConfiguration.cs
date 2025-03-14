using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class StateMasterEntityTypeConfiguration : IEntityTypeConfiguration<StateMaster>
    {
        public void Configure(EntityTypeBuilder<StateMaster> entity)
        {
            entity.ToTable("StateMaster");

            entity.HasIndex(e => e.CountryMasterId, "IX_StateMaster_CountryMasterId");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CountryMaster).WithMany(p => p.StateMasters)
                .HasForeignKey(d => d.CountryMasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StateMaster_CountryMaster");
        }
    }
}
